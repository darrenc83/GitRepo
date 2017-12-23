using Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
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
       // protected Logger Log { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbConnectionFactory">An object that will provide this repository's database connection</param>
        protected BaseRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
           // Log = LogManager.GetLogger(GetType().FullName);
        }

        /// <summary>
        /// Static constructor, contains any relevant initialisation
        /// </summary>
        //static BaseRepository()
        //{
        //    // By default, Dapper maps DateTime to SQL DateTime rather than DateTime2
        //    SqlMapper.AddTypeMap(typeof(DateTime), DbType.DateTime2);
        //}
    }
}
