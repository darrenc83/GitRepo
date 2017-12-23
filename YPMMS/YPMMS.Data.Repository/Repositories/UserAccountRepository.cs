using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPMMS.Data.Repository.Interfaces;
using YPMMS.Shared.Core.Models;
using Dapper;

namespace YPMMS.Data.Repository.Repositories
{
    /// <summary>
    /// Repository for getting <see cref="Collector"/> data from the database
    /// </summary>
    public sealed class UserAccountRepository : BaseRepository
    {
        public UserAccountRepository(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }

        /// <summary>
        /// Retrieve details of an individual user account
        /// </summary>
        /// <param name="collectorId">ID of the collector to retrieve</param>
        /// <returns><see cref="Collector"/> object, or null if the ID was not found</returns>
        public async Task<UserAccount> GetUserAccountAsync(long userAccountId)
        {
            var selectStatement =
                $"select * from UserAccount " +
                $"where {nameof(UserAccount.Id)} = @{nameof(UserAccount.Id)}";

            Log.Trace("Getting collector (Id={0}): {1}", userAccountId, selectStatement);

            var result = await Connection.QueryAsync<UserAccount>(selectStatement, new { Id = userAccountId });
            var userAccount = result.SingleOrDefault();

            if (userAccount == null)
            {
                Log.Warn("No user account found with Id {0}", userAccountId);
            }
            else
            {
                Log.Trace(
                    "Returning user account (Id={0}, Organisation={1}, Department={2}, Created={3}, Logo={4})",
                    userAccount.Id,
                    userAccount.Organisation,
                    userAccount.Department,
                    userAccount.Created,
                    userAccount.Logo);
            }

            return userAccount;
        }
    }
}