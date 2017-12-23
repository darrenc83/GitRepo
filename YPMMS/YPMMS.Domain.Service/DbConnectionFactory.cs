using System.Data;
using System.Data.SqlClient;
using YPMMS.Data.Repository.Interfaces;

namespace YPMMS.Domain.Service
{
    /// <summary>
    /// Initialises a connection to the database on construction and retains it until this
    /// object is disposed. As such, this should normally be constructed in a "using" block.
    /// </summary>
    /// <example>
    /// <code>
    /// using (var connection = new DbConnectionFactory(connectionString)
    /// {
    ///    ...
    /// }
    /// </code>
    /// </example>
    internal sealed class DbConnectionFactory : IDbConnectionFactory
    {
        /// <summary>
        /// Constructor. Created the database connection
        /// </summary>
        /// <param name="connectionString">Connection string to use for the database connection</param>
        public DbConnectionFactory(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Get the database connection owned by this factory object
        /// </summary>
        public IDbConnection Connection { get; }

        /// <summary>
        /// Dispose of this object's database connection
        /// </summary>
        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
