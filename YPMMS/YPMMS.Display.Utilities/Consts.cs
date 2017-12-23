using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace YPMMS.Display.Utilities
{
    /// <summary>
    /// Shared constant values for the website project
    /// </summary>
    public static class Consts
    {
        /// <summary>
        /// Connection string used for the SQL database
        /// </summary>
        public const string DbConnectionString = "DbConnection";

        /// <summary>
        /// Number of system events to show per page
        /// </summary>
        public const int SystemEventsPageSize = 13;

        /// <summary>
        /// Default JSON serialisation settings for the project
        /// </summary>
        public static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            // Format dates as ISO strings (e.g. "2016-07-27T10:00:00Z") rather than the bizarre default Microsoft format
            // (See http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_DateFormatHandling.htm)
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            
            // Convert enum values to strings rather than integers
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter()
            }
        };
    }
}