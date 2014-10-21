using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Mvc.UI
{
    public sealed partial class Sample
    {
        public ToggleClick ToggleClickFor<TModel>(Expression<Func<TModel, bool>> expression) 
        {
            var html = new HtmlHelper<TModel>(_helper.ViewContext, _helper.ViewDataContainer) ;
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
            //var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = true; // bool.Parse(metadata.Model.ToString());
          
            return new ToggleClick(html.ViewContext, fieldId, value, fieldName, fullBindingName);
        }

         public class ToggleClick : JQueryPlugin, IHtmlString
         {
             private string _onLabel = "On";
             private string _offLabel = "Off";
             private bool _value = false;
             private bool _enabled = true;
             private readonly string _fieldName = null;
             private readonly string _fullBindingName = null;

             protected override IClientEventList ClientEventList { get; set; }
             protected override string jQueryPluginName
             {
                 get
                 {
                     return "ToggleClick";
                 }
             }
            
             internal ToggleClick(ViewContext context, string id, bool value, string fieldName, string fullBindingName)
                 : base(context, id)
             {
                 _value = value;
                _fieldName = fieldName;
                _fullBindingName = fullBindingName;

                 ClientEventList = new ToggleClickClientEvents();
             }

             public ToggleClick ID(string id)
             {
                 Guard.IsNotNullOrEmpty(id, "id");
                 _id = id;

                 return this;
             }

             public ToggleClick OnLabel(string label)
             {
                 Guard.IsNotNullOrEmpty(label, "label");
                 _onLabel = label;

                 return this;
             }

             public ToggleClick OffLabel(string label)
             {
                 Guard.IsNotNullOrEmpty(label, "label");
                 _offLabel = label;

                 return this;
             }

             public ToggleClick Enable(bool enable)
             {
                 Guard.IsNotNull(enable, "enable");
                 _enabled = enable;

                 return this;
             }

            /// <summary>
            /// Configures the client-side events.
            /// </summary>
            /// <param name="clientEventsAction">The client events action.</param>
            /// <example>
            /// <code lang="CS">
            ///  &lt;%= Html.Sample().ToggleClick()
            ///             .ClientEvents(events =>
            ///                 events.Toggle("onToggle").OnSwitchOff("onSwitchOff")
            ///             )
            /// %&gt;
            /// </code>
            /// </example>
            public ToggleClick ClientEvents(Action<ToggleClickClientEventsBuilder> clientEventsAction)
            {
                Guard.IsNotNull(clientEventsAction, "clientEventsAction");
                clientEventsAction(new ToggleClickClientEventsBuilder((ToggleClickClientEvents)this.ClientEventList, this._viewContext));

                return this;
            }

            /// <summary>
            /// Creates and returns html elements to render opening tags
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                var wrapper = new TagBuilder("div");
                wrapper.Attributes.Add("id", _id);
                wrapper.AddCssClass("sa-toggle-button");
                wrapper.AddCssClass("sa-widget");
                if (!_enabled)
                {
                    wrapper.AddCssClass("sa-state-disabled");
                }

                var onSwitch = new TagBuilder("span");
                onSwitch.AddCssClass("sa-switch");
                onSwitch.AddCssClass("sa-switch-on");
                onSwitch.SetInnerText(_onLabel);
                if(_value)
                {
                    onSwitch.AddCssClass("sa-state-selected");
                }
                
                var offSwitch = new TagBuilder("span");
                offSwitch.AddCssClass("sa-switch");
                offSwitch.AddCssClass("sa-switch-off");
                offSwitch.SetInnerText(_offLabel);
                if (!_value)
                {
                    offSwitch.AddCssClass("sa-state-selected");
                }

                var hidden = new TagBuilder("input");
                hidden.Attributes.Add("name", _fullBindingName);
                hidden.Attributes.Add("data", "sa-value");
                hidden.Attributes.Add("type", "hidden");
                hidden.Attributes.Add("value", _value.ToString().ToLower());

                wrapper.InnerHtml += onSwitch.ToString();
                wrapper.InnerHtml += offSwitch.ToString();
                wrapper.InnerHtml += hidden.ToString();

                wrapper.InnerHtml += GetInitializationScript(ClientEventList);

                return wrapper.ToString();
            }

            public string ToHtmlString()
            {
                return ToString();
            }
         }
    }
}
