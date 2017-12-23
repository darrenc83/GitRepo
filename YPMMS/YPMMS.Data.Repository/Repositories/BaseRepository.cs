using System;
using System.Data;
using Dapper;
using YPMMS.Data.Repository.Interfaces;
using NLog;

namespace YPMMS.Data.Repository.Repositories
{
    /// <summary>
    /// Base class for the repositories. Implements behaviour required by all repositories.
    /// </summary>
    public abstract class BaseRepository
    {
        /// <summary>
        /// An object passed in at construction that will provide this repository's database connection
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Get the connection from this repository's database connection factory
        /// </summary>
        protected IDbConnection Connection => _dbConnectionFactory.Connection;

        /// <summary>
        /// Log property to be used by all derived classes, following the pattern
        /// described at https://github.com/nlog/nlog/wiki/Tutorial#expose-logger-to-sub-classes
        /// </summary>
        protected Logger Log { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbConnectionFactory">An object that will provide this repository's database connection</param>
        protected BaseRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
            Log = LogManager.GetLogger(GetType().FullName);
        }

        /// <summary>
        /// Static constructor, contains any relevant initialisation
        /// </summary>
        static BaseRepository()
        {
            // By default, Dapper maps DateTime to SQL DateTime rather than DateTime2
            SqlMapper.AddTypeMap(typeof(DateTime), DbType.DateTime2);
        }
    }
}
