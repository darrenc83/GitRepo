using System;
using NLog;

namespace YPMMS.Shared.Core.Extensions
{
    /// <summary>
    /// DateTime extensions for use by the website project
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Default time zone ID to pass to TimeZoneInfo.ConvertTimeFromUtc()
        /// </summary>
        private const string UkTimeZoneId = "GMT Standard Time";

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Convert date/time from UTC to a local time zone
        /// </summary>
        /// <param name="dateTime">UTC date/time to convert</param>
        /// <param name="timeZoneId">ID of the target time zone (defaults to UK)</param>
        /// <returns>Converted date/time, or original UTC date/time if conversion fails</returns>
        public static DateTime ToLocalTimezone(this DateTime dateTime, string timeZoneId = UkTimeZoneId)
        {
            // Skip conversion if it's already a local time
            if (dateTime.Kind == DateTimeKind.Local)
            {
                return dateTime;
            }

            try
            {
                var convertedDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
                return DateTime.SpecifyKind(convertedDateTime, DateTimeKind.Local);
            }
            catch (TimeZoneNotFoundException e)
            {
                Log.Error(e, "Invalid timeZoneId \"{0}\": returning dateTime {1} as supplied", timeZoneId, dateTime);
                return dateTime;
            }
        }

        /// <summary>
        /// Convert date/time from a local or unspecified time zone to UTC.
        /// This is needed because DateTime.ToUniversalTime() doesn't set the Kind to DateTimeKind.Utc.
        /// </summary>
        /// <param name="dateTime">local date/time to convert</param>
        /// <returns>Converted date/time with Kind set to DateTimeKind.Utc</returns>
        public static DateTime ConvertLocalToUtc(this DateTime dateTime)
        {
            // Skip conversion if it's already a UTC time
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime;
            }

            return DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);
        }

        /// <summary>
        /// Take a time we know is UTC (e.g. time read from the database) and specify its kind
        /// as Utc so it gets serialised correctly.
        /// </summary>
        /// <param name="dateTime">UTC date/time</param>
        /// <returns>Same <see cref="DateTime"/> value with its kind set to <see cref="DateTimeKind.Utc"/></returns>
        public static DateTime SpecifyUtc(this DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        }
    }
}