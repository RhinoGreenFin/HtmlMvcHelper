using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Mvc.UI
{
    public abstract class ViewComponent 
    {
        protected readonly ViewContext _viewContext;
        protected string _id;
               
        protected ViewComponent(ViewContext viewContext, string id = null)
        {
            Guard.IsNotNull(viewContext, "viewContext");
             _viewContext = viewContext;

             if (string.IsNullOrWhiteSpace(id))
             {
                 id = "sa-component-" + Guid.NewGuid().ToString();
                 //Note: id values for use with CSS cannot begin with a digit, thus prefix with sa-component
             }
             _id = id;
        }

    }
}
