using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace System
{
    public static class MiscExtensions
    {
        /// <summary>
        /// End time of the day
        /// </summary>
        public static DateTime ToEndTimeOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 999);
        }

        /// <summary>
        /// Start time of the day
        /// </summary>
        public static DateTime ToStartTimeOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0);
        }

        /// <summary>
        /// Returns specified text in Description attribute.
        /// </summary>
        public static string GetDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }


        /// <summary>
        /// Implements fast string replacing algorithm
        /// </summary>
        public static string ReplaceAll(this string str, string pattern, string replacement, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            if (String.IsNullOrEmpty(pattern))
                return str;

            if (replacement == null)
                replacement = string.Empty;

            int lenPattern = pattern.Length;
            int idxPattern = -1;
            int idxLast = 0;

            var result = new StringBuilder();
            try
            {
                while (true)
                {
                    idxPattern = str.IndexOf(pattern, idxPattern + 1, comparisonType);

                    if (idxPattern < 0)
                    {
                        result.Append(str, idxLast, str.Length - idxLast);
                        break;
                    }

                    result.Append(str, idxLast, idxPattern - idxLast);
                    result.Append(replacement);

                    idxLast = idxPattern + lenPattern;
                }
                return result.ToString();
            }
            finally
            {
                result.Length = 0;
            }
        }

    }
}
