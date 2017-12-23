using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YPMMS.Data.Repository.Repositories;
using YPMMS.Shared.Core.Models;

namespace YPMMS.Domain.Service.Services
{
    /// <summary>
    /// Service to retrieve collector data from the data repositories
    /// </summary>
    public sealed class UserAccountDataService : BaseDataService
    {
        public UserAccountDataService(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// Retrieve a single userAccount record
        /// </summary>
        /// <param name="userAccountId">ID of the collector to retrieve</param>
        /// <returns><see cref="UserAccount"/> object</returns>
        public async Task<UserAccount> GetUserAccountAsync(long userAccountId)
        {
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                // Get the collector
                var userAccountRepository = new UserAccountRepository(connectionFactory);
                var userAccount = await userAccountRepository.GetUserAccountAsync(userAccountId);
                return userAccount;
            }
        }

       
    }
}
