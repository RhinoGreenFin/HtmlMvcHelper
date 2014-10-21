using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Sample.Web.Mvc.UI
{
    public abstract class JQueryPlugin : ViewComponent
    {
        protected abstract string jQueryPluginName { get; }
        protected abstract IClientEventList ClientEventList { get; set; }
        private ClientSideObjectWriter _writer;
        protected ClientSideObjectWriter ClientWriter
        {
            get { return _writer ?? (_writer = new ClientSideObjectWriter(_id, jQueryPluginName)); }
        }

        protected JQueryPlugin(ViewContext viewContext, string id = null) : base(viewContext, id)
        {
            
        }

        protected string GetInitializationScript(IClientEventList clientEventList)
        {
            return GetInitializationScript(clientEventList, null);
        }

        protected string GetInitializationScript(IClientEventList clientEventList, IClientEventList ajaxEventList)
        {
            Guard.IsNotNull(clientEventList, "clientEventList");
            Guard.IsNotNullOrEmpty(jQueryPluginName, "jQueryPluginName");
            
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(clientEventList))
            {
                var jsEventName = Char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1); //turns OnClick to onClick
                ClientWriter.AppendClientEvent(jsEventName, (ClientEvent)prop.GetValue(clientEventList));
            }

            if (ajaxEventList != null)
            {
                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(ajaxEventList))
                {
                    var jsEventName = Char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1); //turns OnClick to onClick
                    ClientWriter.AppendClientEvent(jsEventName, (ClientEvent)prop.GetValue(ajaxEventList));
                }
            }

            var script = new TagBuilder("script");
            script.Attributes.Add("type", "text/javascript");

            script.InnerHtml = _writer.ToHtmlString();
            return script.ToString();
        }
    }
}
