using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using YPMMS.Data.Repository.Interfaces;
using YPMMS.Data.Repository.Models;
using YPMMS.Shared.Core.Models;

namespace YPMMS.Data.Repository.Repositories
{
    /// <summary>
    /// Repository for getting <see cref="SystemEvent"/> data from the database
    /// </summary>
    public sealed class SystemEventRepository : BaseRepository
    {
        public SystemEventRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        /// <summary>
        /// Get a set of system events for a user
        /// </summary>
        /// <param name="userId">ID of the current user</param>
        /// <param name="countToReturn">Maximum number of events to return</param>
        /// <param name="beforeId">Only return events with IDs less than the specified value</param>
        /// <param name="fromDateTime">Only return events from this date/time</param>
        /// <param name="toDateTime">Only return events to this date/time</param>
        /// <returns></returns>
        public async Task<IList<SystemEvent>> GetSystemEventsForUserAsync(
            string userId,
            int countToReturn,
            long beforeId = 0,
            DateTimeOffset? fromDateTime = null,
            DateTimeOffset? toDateTime = null)
        {
            Log.Trace("Getting system events for user {0} (countToReturn={1}, beforeId={2}, fromDateTime={3}, toDateTime={4})",
                userId, countToReturn, beforeId, fromDateTime, toDateTime);

            var sprocParams = new
            {
                userId,
                countToReturn,
                beforeId,
                fromDateTime,
                toDateTime
            };

            var results = await Connection.QueryAsync<SystemEvent>(
                Consts.DbSprocs.GetSystemEventsForUser,
                sprocParams,
                commandType: CommandType.StoredProcedure);
            var events = results.ToList();
            Log.Trace("Returning {0} system events", events.Count);

            return events;
        }

        /// <summary>
        /// Get a set of system events for a machine
        /// </summary>
        /// <param name="machineId">ID of the machine to get events for</param>
        /// <param name="userId">ID of the current user (used to detect new events)</param>
        /// <param name="countToReturn">Maximum number of events to return</param>
        /// <param name="beforeId">Only return events with IDs less than the specified value</param>
        /// <param name="fromDateTime">Only return events from this date/time</param>
        /// <param name="toDateTime">Only return events to this date/time</param>
        /// <returns></returns>
        public async Task<IList<SystemEvent>> GetSystemEventsForMachineAsync(
            long machineId,
            string userId,
            int countToReturn,
            long beforeId = 0,
            DateTimeOffset? fromDateTime = null,
            DateTimeOffset? toDateTime = null)
        {
            Log.Trace("Getting system events for machine {0} (userId={1}, countToReturn={2}, beforeId={3}, fromDateTime={4}, toDateTime={5})",
                machineId, userId, countToReturn, beforeId, fromDateTime, toDateTime);

            var sprocParams = new
            {
                machineId,
                userId,
                countToReturn,
                beforeId,
                fromDateTime,
                toDateTime
            };

            var results = await Connection.QueryAsync<SystemEvent>(
                Consts.DbSprocs.GetSystemEventsForMachine,
                sprocParams,
                commandType: CommandType.StoredProcedure);

            return results.ToList();
        }




        /// <summary>
        /// Gets a StatusEvent record from the database.
        /// </summary>
        /// <param name="stausEventId"> The id of the Item required </param>
        /// <returns></returns>
        public async Task <StatusEvent>GetStatusEventAsync(long statusEventId)
        {

            var selectStatement = "select * from StatusEvent where Id=@Id";

            var result = await Connection.QueryAsync<StatusEvent>(selectStatement, new { Id = statusEventId });

            return result.SingleOrDefault();

        }


        /// <summary>
        /// Add a new system event to the database.
        /// </summary>
        /// <param name="systemEvent">Event to add. The Id and Timestamp properties will be
        /// ignored and assigned to defaults in the database.</param>
        /// <returns></returns>
        public async Task AddSystemEventAsync(SystemEvent systemEvent)
        {
            var sprocParams = Mapper.Map<SystemEventInsertModel>(systemEvent);
            await Connection.ExecuteAsync(
                Consts.DbSprocs.CreateSystemEvent,
                sprocParams,
                commandType: CommandType.StoredProcedure);
        }
    }
}