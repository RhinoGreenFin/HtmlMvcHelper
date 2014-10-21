namespace Sample.Web.Mvc
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Helper class for argument validation.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Ensures the specified argument is not null.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void IsNotNull(object parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName, "Cannot be null");
            }
        }

        /// <summary>
        /// Ensures the specified string is not blank.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void IsNotNullOrEmpty(string parameter, string parameterName)
        {
            if (string.IsNullOrEmpty((parameter ?? string.Empty)))
            {
                throw new ArgumentException("Cannot be null or empty");
            }
        }

        /// <summary>
        /// Ensures the specified object is a certain type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void IsTypeOf(TypeCode type, object parameter, string parameterName)
        {
            Guard.IsNotNull(parameter, parameterName);
           
            if (System.Type.GetTypeCode(parameter.GetType()) != type)
            {
                throw new ArgumentException("Must be of type " + type.ToString());
            }
        }

        /// <summary>
        /// Ensures the specified array is not null or empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter">The parameter.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void IsNotNullOrEmpty<T>(T[] parameter, string parameterName)
        {
            IsNotNull(parameter, parameterName);
            
            if (parameter.Length == 0)
            {
                throw new ArgumentException("Cannot be null or empty");
            }
        }

        /// <summary>
        /// Ensures the specified collection is not null or empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter">The parameter.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void IsNotNullOrEmpty<T>(ICollection<T> parameter, string parameterName)
        {
            IsNotNull(parameter, parameterName);

            if (parameter.Count == 0)
            {
                throw new ArgumentException("Cannot be null or empty", parameterName);
            }
        }

        /// <summary>
        /// Ensures the specified value is a positive integer.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void IsNotZeroOrNegative(int parameter, string parameterName)
        {
            if (parameter <= 0)
            {
                throw new ArgumentOutOfRangeException(parameterName, "Must be greater than zero");
            }
        }

        /// <summary>
        /// Ensures the specified value is not a negative integer.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void IsNotNegative(int parameter, string parameterName)
        {
            if (parameter < 0)
            {
                throw new ArgumentOutOfRangeException(parameterName, "Must be positive number");
            }
        }

        /// <summary>
        /// Ensures the specified path is a virtual path which starts with ~/.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void IsNotVirtualPath(string parameter, string parameterName)
        {
            IsNotNullOrEmpty(parameter, parameterName);

            if (!parameter.StartsWith("~/", StringComparison.Ordinal))
            {
                throw new ArgumentException("Must be a virtual path and should start with a ~/", parameterName);
            }
        }
    }
}