using System;
using System.Threading;
using YPMMS.Display.Utilities.Extensions;
using YPMMS.Shared.Core.Extensions;

namespace YPMMS.Display.Utilities
{
    /// <summary>
    /// Static helper methods to format values for display
    /// </summary>
    public static class Format
    {
        /// <summary>
        /// Format a decimal value as a currency value, i.e. to 2 decimal places, thousands comma-separated,
        /// and optionally prepended with a currency symbol or abbreviation
        /// </summary>
        /// <param name="value">Value to format</param>
        /// <param name="currency">Currency to display, or null to omit</param>
        /// <param name="emptyStringForZero">If true, return an empty string for a zero value; if false, return a formatted string (e.g. "£0.00")</param>
        /// <returns>Formatted string</returns>
        public static string AsCurrency(decimal value, string currency = null, bool emptyStringForZero = false)
        {
            if (value == 0M && emptyStringForZero)
            {
                return string.Empty;
            }

            return $"{currency?.ToCurrencySymbol()}{value:N2}";
        }

        /// <summary>
        /// Convert a <see cref="DateTime"/> to a local value and format it as a short date (e.g. "DD/MM/YYYY")
        /// </summary>
        /// <param name="dateTime">Value to format</param>
        /// <returns>Formatted string</returns>
        public static string AsDate(DateTime dateTime)
        {
            // It's OK to use Thread.CurrentThread.CurrentCulture here as long as we've included
            // <globalization enableClientBasedCulture="true" uiCulture="auto" culture="auto"/> in Web.config
            // (see http://www.hanselman.com/blog/GlobalizationInternationalizationAndLocalizationInASPNETMVC3JavaScriptAndJQueryPart1.aspx)
            return dateTime.ToLocalTimezone().ToString("d", Thread.CurrentThread.CurrentCulture);
        }

        /// <summary>
        /// Convert a <see cref="DateTime"/> to a local value and format it as a string in the format "(h)h:mm:ss AM/PM" (in 12-hour clock)
        /// </summary>
        /// <param name="dateTime">Value to format</param>
        /// <returns>Formatted string</returns>
        public static string AsTime(DateTime dateTime)
        {
            return dateTime.ToLocalTimezone().ToString("h:mm:ss tt");
        }

        /// <summary>
        /// Convert a <see cref="string"/> to a integer value
        /// </summary>
        /// <param name="dateTime">Value to format</param>
        /// <returns>Formatted string</returns>
        public static long AsLong(string str)
        {
            return Convert.ToInt64(str);      
        }

    }
}