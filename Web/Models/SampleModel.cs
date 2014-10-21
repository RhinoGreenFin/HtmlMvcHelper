using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examples.Models
{
    public class SampleModel
    {
        public string Name { get; set; }
        public bool ToggleProperty { get; set; }
        public SampleSubClass SubClass { get; set; }

        public SampleModel()
        {
            SubClass = new SampleSubClass();
        }
    }

    public class SampleSubClass
    {
        public bool BooleanSubProperty { get; set; }
    }
}