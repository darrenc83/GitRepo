using Data.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace YPSWebApp.Domain.Service
{
    internal sealed class DbConnectionFactory :IDbConnectionFactory
    {
        /// <summary>
        /// Constructor. Created the database connection
        /// </summary>
        /// <param name="connectionString">Connection string to use for the database connection</param>
        public DbConnectionFactory(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Command = new SqlCommand();
            
        }

        public IDbCommand Command { get; set; }


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
