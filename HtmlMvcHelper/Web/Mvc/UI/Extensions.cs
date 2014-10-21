using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Globalization;

namespace Sample.Web.Mvc.UI
{
    public static partial class Extensions
    {
        // Extension method - adds a Sample() method to HtmlHelper
        public static Sample Sample(this HtmlHelper helper)
        {
            return new Sample(helper);
        }
    }

    public sealed partial class Sample
    {
        private readonly HtmlHelper _helper = null;

        public Sample(HtmlHelper helper)
        {
            _helper = helper;
        }
    }


    public static class StringExtensions
    {
        /// <summary>
        /// Replaces the format item in a specified System.String with the text equivalent of the value of a corresponding System.Object instance in a specified array.
        /// </summary>
        /// <param name="instance">A string to format.</param>
        /// <param name="args">An System.Object array containing zero or more objects to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the System.String equivalent of the corresponding instances of System.Object in args.</returns>
        public static string FormatWith(this string instance, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, instance, args);
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}
