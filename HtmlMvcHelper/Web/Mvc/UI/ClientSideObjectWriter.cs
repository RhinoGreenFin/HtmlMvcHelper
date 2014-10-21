
namespace Sample.Web.Mvc.UI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Script.Serialization;

    /// <summary>
    /// Class used to build initialization script of jQuery plugin.
    /// </summary>
    public class ClientSideObjectWriter : IHtmlString 
    {
        private readonly string id;
        private readonly string type;
        private readonly StringBuilder writer;
        private bool appended;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientSideObjectWriter"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="type">The type.</param>
        /// <param name="textWriter">The text writer.</param>
        public ClientSideObjectWriter(string id, string type) 
        {
            Guard.IsNotNullOrEmpty(id, "id");
            Guard.IsNotNullOrEmpty(type, "type");
           
            this.id = id;
            this.type = type;
            writer = new StringBuilder();

            //Escape meta characters: http://api.jquery.com/category/selectors/
            var selector = @";&,.+*~':""!^$[]()|/".ToCharArray().Aggregate(id, (current, chr) => current.Replace(chr.ToString(), @"\\" + chr));

            writer.Append("jQuery('#{0}').Sample().{1}(".FormatWith(selector, type));
        }


        /// <summary>
        /// Appends the specified key value pair to the end of this instance.
        /// </summary>
        /// <param name="keyValuePair">The key value pair.</param>
        /// <returns></returns>
        public virtual ClientSideObjectWriter Append(string keyValuePair)
        {
            if (!string.IsNullOrEmpty(keyValuePair))
            {
                writer.Append(appended ? ", " : "{");
                writer.Append(keyValuePair);

                if (!appended)
                {
                    appended = true;
                }
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ClientSideObjectWriter Append(string name, string value)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            if (value.HasValue())
            {
                string formattedValue = QuoteString(value);

                Append("{0}:'{1}'".FormatWith(name, formattedValue));
            }

            return this;
        }


        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ClientSideObjectWriter Append(string name, int value)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            Append("{0}:{1}".FormatWith(name, value));

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public virtual ClientSideObjectWriter Append(string name, int value, int defaultValue)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            if (value != defaultValue)
            {
                Append(name, value);
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ClientSideObjectWriter Append(string name, int? value)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            if (value.HasValue)
            {
                Append(name, value.Value);
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ClientSideObjectWriter Append(string name, double value)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            Append("{0}:'{1}'".FormatWith(name, value));

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ClientSideObjectWriter Append(string name, double? value)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            if (value.HasValue)
            {
                Append(name, value.Value);
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ClientSideObjectWriter Append(string name, decimal value)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            Append("{0}:'{1}'".FormatWith(name, value));

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ClientSideObjectWriter Append(string name, decimal? value)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            if (value.HasValue)
            {
                Append(name, value.Value);
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual ClientSideObjectWriter Append(string name, bool value)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            Append("{0}:{1}".FormatWith(name, value.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture)));

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns></returns>
        public virtual ClientSideObjectWriter Append(string name, bool value, bool defaultValue)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            if (value != defaultValue)
            {
                Append(name, value);
            }

            return this;
        }


        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ClientSideObjectWriter Append(string name, DateTime value)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            if (value != DateTime.MinValue)
            {
                string dateValue = "new Date({0},{1},{2},{3},{4},{5},{6})".FormatWith(value.Year.ToString("###0", CultureInfo.InvariantCulture), (value.Month - 1).ToString("#0", CultureInfo.InvariantCulture), value.Day.ToString("#0", CultureInfo.InvariantCulture), value.Hour.ToString("#0", CultureInfo.InvariantCulture), value.Minute.ToString("#0", CultureInfo.InvariantCulture), value.Second.ToString("#0", CultureInfo.InvariantCulture), value.Millisecond.ToString("##0", CultureInfo.InvariantCulture));

                Append("{0}:{1}".FormatWith(name, dateValue));
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual ClientSideObjectWriter Append(string name, DateTime? value)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            if (value.HasValue)
            {
                Append(name, value.Value);
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public virtual ClientSideObjectWriter Append(string name, Action action)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            if (action != null)
            {
                Append("{0}:".FormatWith(name));
                action();
            }

            return this;
        }

        public virtual ClientSideObjectWriter AppendCollection(string name, IEnumerable value)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            return Append("{0}:{1}".FormatWith(name, new JavaScriptSerializer().Serialize(value)));
        }

        public virtual ClientSideObjectWriter AppendObject(string name, object value)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            return Append("{0}:{1}".FormatWith(name, new JavaScriptSerializer().Serialize(value)));
        }

        /// <summary>
        /// Appends the specified name and Action or String specified in the ClientEvent object.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="event">Client event of the component.</para>
        /// <returns></returns>
        public virtual ClientSideObjectWriter AppendClientEvent(string name, ClientEvent clientEvent)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            if (clientEvent.HandlerName.HasValue())
            {
                Append("{0}:{1}".FormatWith(name, clientEvent.HandlerName));
            }

            return this;
        }

        public string ToHtmlString()
        {
            if (appended)
            {
                writer.Append("}");
            }

            writer.Append(");");

            return writer.ToString();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Needs refactoring")]
        private static string QuoteString(string value)
        {
            StringBuilder result = new StringBuilder();

            if (!string.IsNullOrEmpty(value))
            {
                int startIndex = 0;
                int count = 0;

                for (int i = 0; i < value.Length; i++)
                {
                    char c = value[i];

                    if (c == '\r' || c == '\t' || c == '\"' || c == '\'' || c == '<' || c == '>' ||
                        c == '\\' || c == '\n' || c == '\b' || c == '\f' || c < ' ')
                    {
                        if (count > 0)
                        {
                            result.Append(value, startIndex, count);
                        }

                        startIndex = i + 1;
                        count = 0;
                    }

                    switch (c)
                    {
                        case '\r':
                            result.Append("\\r");
                            break;
                        case '\t':
                            result.Append("\\t");
                            break;
                        case '\"':
                            result.Append("\\\"");
                            break;
                        case '\\':
                            result.Append("\\\\");
                            break;
                        case '\n':
                            result.Append("\\n");
                            break;
                        case '\b':
                            result.Append("\\b");
                            break;
                        case '\f':
                            result.Append("\\f");
                            break;
                        case '\'':
                        case '>':
                        case '<':
                            AppendAsUnicode(result, c);
                            break;
                        default:
                            if (c < ' ')
                            {
                                AppendAsUnicode(result, c);
                            }
                            else
                            {
                                count++;
                            }

                            break;
                    }
                }

                if (result.Length == 0)
                {
                    result.Append(value);
                }
                else if (count > 0)
                {
                    result.Append(value, startIndex, count);
                }
            }

            return result.ToString();
        }

        private static void AppendAsUnicode(StringBuilder builder, char c)
        {
            builder.Append("\\u");
            builder.AppendFormat(CultureInfo.InvariantCulture, "{0:x4}", (int)c);
        }
    }
}