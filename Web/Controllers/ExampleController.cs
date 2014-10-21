using System.Threading;
using Examples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examples.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Example
        public ActionResult Index()
        {

            var model = new SampleModel()
            {
                Name = "Hello",
                ToggleProperty = true
            };

            return View(model);         
        }
    }
}