using System.Configuration;
using YPMMS.Display.Utilities;

namespace YPMMS.Display.Website
{
    /// <summary>
    /// Shared configuration properties and methods
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// Connection string for the SQL database
        /// </summary>
        public static string DbConnectionString => ConfigurationManager.ConnectionStrings[Consts.DbConnectionString].ConnectionString;
    }
}