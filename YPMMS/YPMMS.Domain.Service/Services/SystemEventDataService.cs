using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YPMMS.Data.Repository.Repositories;
using YPMMS.Shared.Core.Enums;
using YPMMS.Shared.Core.Models;

namespace YPMMS.Domain.Service.Services
{
    /// <summary>
    /// Service to retrieve <see cref="SystemEvent"/> data from, and add it to, the data repositories
    /// </summary>
    public sealed class SystemEventDataService : BaseDataService
    {
        public SystemEventDataService(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Get system events for a user, optionally between two dates
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
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                var repository = new SystemEventRepository(connectionFactory);
                return await repository.GetSystemEventsForUserAsync(
                    userId,
                    countToReturn,
                    beforeId,
                    fromDateTime,
                    toDateTime);
            }
        }


        /// <summary>
        /// Get The Status event item added by the socket
        /// </summary>
        /// <param name="systemEventId">ID of the StatusEvent</param>
        /// <returns></returns>
        public async Task<StatusEvent> GetStatusEventAsync(long systemEventId)
        {
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                var repository = new SystemEventRepository(connectionFactory);
                return await repository.GetStatusEventAsync(systemEventId);
            }
        }


        /// <summary>
        /// Get latest system events for a user, e.g. for showing in the system notifications dropdown
        /// </summary>
        /// <param name="userId">ID of the current user</param>
        /// <param name="countToReturn">Maximum number of events to return</param>
        /// <returns></returns>
        public async Task<IList<SystemEvent>> GetLatestSystemEventsForUserAsync(string userId, int countToReturn)
        {
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                var repository = new SystemEventRepository(connectionFactory);
                return await repository.GetSystemEventsForUserAsync(userId, countToReturn);
            }
        }

        /// <summary>
        /// Get system events for a machine, optionally between two dates
        /// </summary>
        /// <param name="machineId">ID of machine to get events for</param>
        /// <param name="userId">ID of the current user</param>
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
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                var repository = new SystemEventRepository(connectionFactory);
                return await repository.GetSystemEventsForMachineAsync(
                    machineId,
                    userId,
                    countToReturn,
                    beforeId,
                    fromDateTime,
                    toDateTime);
            }
        }

        /// <summary>
        /// Add a system event when a new machine is added
        /// </summary>
        /// <param name="machineId">ID of the new machine</param>
        /// <param name="siteId">ID of the site the machine was added to</param>
        /// <param name="userId">ID of the user who added the machine</param>
        /// <returns></returns>
        public async Task AddMachineAddedEventAsync(long machineId, long siteId, string userId)
        {
            var machineAddedEvent = new SystemEvent
            {
                EventType = EventType.MachineAdded,
                MachineId = machineId,
                UserId = userId
            };

            var machineAssignedToSiteEvent = new SystemEvent
            {
                EventType = EventType.MachineAssignedToSite,
                MachineId = machineId,
                SiteId = siteId,
                UserId = userId
            };

            await AddEventsAsync(machineAddedEvent, machineAssignedToSiteEvent);
        }

        /// <summary>
        /// Add a system event when one or more machines are removed
        /// </summary>
        /// <param name="machines">Machine(s) that were removed</param>
        /// <param name="userId">ID of the user who removed the machines</param>
        /// <returns></returns>
        public async Task AddMachineRemovedEventsAsync(IList<Merchant> machines, string userId)
        {
            var machinesRemovedEvents = machines.Select(m => new SystemEvent
            {
                EventType = EventType.MachineRemoved,
                MachineId = m.Id,
                //MachineName = m.Name,
                //SiteId = m.SiteId,
                //SiteName = m.Site?.Name,
                UserId = userId
            }).ToArray();

            await AddEventsAsync(machinesRemovedEvents);
        }

        /// <summary>
        /// Add a system event when a machine's details are edited
        /// </summary>
        /// <param name="machineId">The ID of the machine</param>
        /// <param name="userId">ID of the user who edited the machine</param>
        /// <param name="newSiteId">ID of the machine's new site if it's moved, otherwise 0</param>
        /// <returns></returns>
        public async Task AddMachineDetailsEditedEventAsync(long machineId, string userId, long newSiteId = 0)
        {
            var events = new List<SystemEvent>
            {
                new SystemEvent
                {
                    EventType = EventType.MachineDetailsEdited,
                    MachineId = machineId,
                    UserId = userId
                }
            };


            if (newSiteId != 0)
            {
                events.Add(new SystemEvent
                {
                    EventType = EventType.MachineAssignedToSite,
                    MachineId = machineId,
                    SiteId = newSiteId,
                    UserId = userId
                });
            }

            await AddEventsAsync(events.ToArray());
        }

        /// <summary>
        /// Add a system event when a site is added
        /// </summary>
        /// <param name="siteId">ID of the new site</param>
        /// <param name="userId">ID of the user who added the site</param>
        /// <returns></returns>
        public async Task AddSiteAddedEventAsync(long siteId, string userId)
        {
            var siteAddedEvent = new SystemEvent
            {
                EventType = EventType.SiteAdded,
                SiteId = siteId,
                UserId = userId
            };

            await AddEventsAsync(siteAddedEvent);
        }

        /// <summary>
        /// Add a system event when a site is removed
        /// </summary>
        /// <param name="siteId">ID of the site that was removed</param>
        /// <param name="userId">ID of the user who removed the site</param>
        /// <returns></returns>
        public async Task AddSiteRemovedEventAsync(long siteId, string userId)
        {
            var siteRemovedEvent = new SystemEvent
            {
                EventType = EventType.SiteRemoved,
                SiteId = siteId,
                UserId = userId
            };

            await AddEventsAsync(siteRemovedEvent);
        }

        /// <summary>
        /// Add a system event when a collection event occurs
        /// </summary>
        /// <param name="collectionEventId">ID of the collectionEvent</param>
        /// <returns></returns>
     

        /// <summary>
        /// Add a system event when a status event change occurs
        /// </summary
        /// <param name="statusEventId">ID of the StatusEvent </param>
        /// <returns></returns>
        public async Task AddStatusEventAsync(long statusEventId)
        {
            var statusEventSevice = new SystemEventDataService(ConnectionString);
            var statusEvent = await statusEventSevice.GetStatusEventAsync(statusEventId);

            var siteRemovedEvent = new SystemEvent
            {
                MachineId = statusEvent.MachineId,
                EventType = (EventType)Enum.Parse(typeof(EventType), statusEvent.EventType)
            };

            await AddEventsAsync(siteRemovedEvent);
        }


        /// <summary>
        /// Add one or more system event
        /// </summary>
        /// <param name="systemEvents">System event(s) to add</param>
        /// <returns></returns>
        private async Task AddEventsAsync(params SystemEvent[] systemEvents)
        {
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                var repository = new SystemEventRepository(connectionFactory);

                foreach (var systemEvent in systemEvents)
                {
                    await repository.AddSystemEventAsync(systemEvent);
                }
            }
        }
    }
}