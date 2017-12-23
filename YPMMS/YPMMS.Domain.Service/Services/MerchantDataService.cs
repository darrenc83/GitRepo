using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YPMMS.Data.Repository.Repositories;

using YPMMS.Shared.Core.Models;

namespace YPMMS.Domain.Service.Services
{
    /// <summary>
    /// Service to retrieve <see cref="Machine"/> data from the data repositories
    /// </summary>
    public sealed class MerchantDataService : BaseDataService
    {
        public MerchantDataService(string connectionString)
            : base(connectionString)
        {
        }



        public async Task<Merchant> GetMerchantAsync(long merchantId)
        {
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                // Get the machines
                var merchanteRepository = new MerchantRepository(connectionFactory);
                var merchant = await merchanteRepository.GetMerchantAsync(merchantId);
                return merchant;
            }

        }



        /// <summary>
        /// Add a new machine to the database
        /// </summary>
        /// <param name="machine"><see cref="Machine"/> object to add to the database</param>
        //public async Task AddMachineAsync(Machine machine)
        //{
        //    using (var connectionFactory = new DbConnectionFactory(ConnectionString))
        //    {
        //        var machineRepository = new MachineRepository(connectionFactory);
        //        await machineRepository.AddMachineAsync(machine);
        //    }
        //}

        /// <summary>
        /// Edit an existing machine in the database
        /// </summary>
        /// <param name="machine"><see cref="Machine"/> object to update in the database</param>
        //public async Task EditMachineAsync(Machine machine)
        //{
        //    using (var connectionFactory = new DbConnectionFactory(ConnectionString))
        //    {
        //        var machineRepository = new MachineRepository(connectionFactory);
        //        await machineRepository.EditMachineAsync(machine);
        //    }
        //}

        /// <summary>
        /// Get an individual machine
        /// </summary>
        /// <param name="machineId">ID of the machine to get</param>
        /// <returns><see cref="Machine"/> object</returns>
        //public async Task<Machine> GetMachineAsync(long machineId)
        //{
        //    return await BuildMachineDataAsync(machineId);
        //}


        //public async Task<BactaInfo> GetBactaDataAsync(long machineId)
        //{
        //    using (var connectionFactory = new DbConnectionFactory(ConnectionString))
        //    {
        //        var bactaRepository = new BactaRepository(connectionFactory);
        //        return await bactaRepository.GetInfo(machineId);
        //    }
        //}


        //public async Task<TebsBag> GetMachineBagAsync(long machineId)
        //{
        //    using (var connectionFactory = new DbConnectionFactory(ConnectionString))
        //    {
        //        var tebsrep = new TebsRepository(connectionFactory);
        //        TebsBag tbag = new TebsBag();
        //        tbag.code =  await tebsrep.GetCurrentBag(machineId);
        //        tbag.total = await tebsrep.GetBagTotal(tbag.code.Code);
        //        tbag.count = await tebsrep.GetBagCounts(tbag.code.Code);
        //        tbag.LastCollection = await tebsrep.GetLastCollection(machineId);
        //        return tbag;
        //    }
        //}


        //public async Task<TebsBag> GetTebsBagAsync(string tebsCode)
        //{
        //    using (var connectionFactory = new DbConnectionFactory(ConnectionString))
        //    {
        //        var tebsrep = new TebsRepository(connectionFactory);
        //        var baghistory = await tebsrep.GetBagHistory(tebsCode);
        //        baghistory.total = await tebsrep.GetBagTotal(tebsCode);
        //        baghistory.count = await tebsrep.GetBagCounts(tebsCode);
        //        baghistory.buttonRecords = await tebsrep.GetBagButtonRecords(tebsCode);
        //        baghistory.sessionGroups = tebsrep.GetSessionGroups(baghistory.buttonRecords);
        //        return baghistory;
        //    }
        //}




        /// <summary>
        /// Get a machine with all its recent collections
        /// </summary>
        /// <param name="machineId">ID of the machine to get</param>
        /// <param name="fromDateTime">Get events from this date (inclusive)</param>
        /// <param name="toDateTime">Get events to this date/time (inclusive)</param>
        /// <returns><see cref="Machine"/> object</returns>
        //public async Task<Machine> GetMachineWithCollectionsAsync(
        //    long machineId,
        //    DateTime? fromDateTime = null,
        //    DateTime? toDateTime = null)
        //{
        //    return await BuildMachineDataAsync(machineId, true, fromDateTime, toDateTime);
        //}

        /// <summary>
        /// Get all machines, sorted by name (ascending)
        /// </summary>
        /// <returns>List of <see cref="Merchant"/> objects</returns>
        public async Task<IList<Merchant>> GetAllMerchantsAsync()
        {
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                // Get the machines
                var merchanteRepository = new MerchantRepository(connectionFactory);
                var merchants = await merchanteRepository.GetMerchantsAsync();
                return merchants;
            }
        }

        public async Task<bool> DoesMerchantExistAsync(long merchantId)
        {
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                var merchanteRepository = new MerchantRepository(connectionFactory);
                return await merchanteRepository.DoesMerchantExistAsync(merchantId);
            };
        }

        public async Task<bool> DoesMachineNameExistAsync(string merchantName, long? ignoreMerchantId)
        {
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                var merchanteRepository = new MerchantRepository(connectionFactory);
                return await merchanteRepository.DoesMachineNameExistAsync(merchantName, ignoreMerchantId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>    
        //public async Task<OverallTotal> GetOverallDataAsync(long managerId = 0, long account = 0)
        //{
        //    using (var connectionFactory = new DbConnectionFactory(ConnectionString))
        //    {
        //        // Get the machines
        //        var machineRepository = new MerchantRepository(connectionFactory);
        //        var machines = await machineRepository.GetTotalsAsync(managerId, account);
        //        return machines;
        //    }
        //}

        /// <summary>
        /// Get machines by ID
        /// </summary>
        /// <returns>List of <see cref="Machine"/> objects</returns>
        //public async Task<IList<Machine>> GetMachinesAsync(IList<long> machineIds)
        //{
        //    using (var connectionFactory = new DbConnectionFactory(ConnectionString))
        //    {
        //        // Get the machines
        //        var machineRepository = new MachineRepository(connectionFactory);
        //        var machines = await machineRepository.GetMachinesAsync(machineIds);
        //        return machines;
        //    }
        //}

        /// <summary>
        /// Retrieve just the latest status information for a machine
        /// </summary>
        /// <param name="machineId"></param>
        /// <returns></returns>
        //public async Task<MachineStatusData> GetMachineStatusAsync(long machineId, long account)
        //{
        //    using (var connectionFactory = new DbConnectionFactory(ConnectionString))
        //    {
        //        // Get the machines
        //        var machineRepository = new MachineRepository(connectionFactory);
        //        var machine = await machineRepository.GetMachineAsync(machineId);

        //        if (machine == null)
        //        {
        //            return null;
        //        }

        //        var machineStatuses = await machineRepository.GetMachineStatusesAsync(account);

        //        return new MachineStatusData
        //        {
        //            Machine = machine,
        //            OfflineCount = machineStatuses.OfflineCount,
        //            FullCount = machineStatuses.FullCount,
        //            ErrorCount = machineStatuses.ErrorCount,
        //            UpdateCount = machineStatuses.UpdateCount
        //        };
        //    }
        //}

        /// <summary>
        /// Get a machine and the current system total values
        /// </summary>
        /// <param name="machineId">ID of the machine to retrieve</param>
        /// <returns></returns>
        //public async Task<MachineAndSystemTotalsData> GetMachineAndSystemTotalsAsync(long machineId, long account)
        //{
        //    using (var connectionFactory = new DbConnectionFactory(ConnectionString))
        //    {
        //        var machineRepository = new MachineRepository(connectionFactory);
        //        var machine = await machineRepository.GetMachineAsync(machineId);
        //        if (machine == null)
        //        {
        //            return null;
        //        }

        //        var machineDenominationRepository = new MachineDenominationRepository(connectionFactory);
        //        var systemTotals = await machineDenominationRepository.GetSystemTotalValuesAsync(account);

        //        return new MachineAndSystemTotalsData
        //        {
        //            Machine = machine,
        //            AllMachinesCollectableValue = systemTotals.TotalCollectableValue
        //        };
        //    }
        //}




        /// <summary>
        /// Check if a machine ID already exists in the database
        /// </summary>
        /// <param name="machineId">ID to check</param>
        /// <returns>True if the ID exists</returns>
        //public async Task<bool> DoesMachineIdExistAsync(long machineId)
        //{
        //    using (var connectionFactory = new DbConnectionFactory(ConnectionString))
        //    {
        //        var machineRepository = new MerchantRepository(connectionFactory);
        //        return await machineRepository.DoesMachineIdExistAsync(machineId);
        //    }
        //}

        /// <summary>
        /// Check if a machine name already exists in the database
        /// </summary>
        /// <param name="machineName">Name to check</param>
        /// <param name="ignoreMachineId">ID of machineto ignore when checking for duplicates</param>
        /// <returns>True if the name exists</returns>
        //public async Task<bool> DoesMachineNameExistAsync(string machineName, long? ignoreMachineId)
        //{
        //    using (var connectionFactory = new DbConnectionFactory(ConnectionString))
        //    {
        //        var machineRepository = new MachineRepository(connectionFactory);
        //        return await machineRepository.DoesMachineNameExistAsync(machineName, ignoreMachineId);
        //    }
        //}

        /// <summary>
        /// Delete one or more machine from the database
        /// </summary>
        /// <param name="machineIds">List of machine IDs. If null or empty, this method is a no-op</param>
        /// <returns>Machines that were deleted</returns>
        //public async Task<IList<Machine>>  DeleteMachinesAsync(IList<long> machineIds)
        //{
        //    using (var connectionFactory = new DbConnectionFactory(ConnectionString))
        //    {
        //        var machineRepository = new MachineRepository(connectionFactory);
        //        return await machineRepository.DeleteMachinesAsync(machineIds);
        //    }
        //}

        //private async Task<Machine> BuildMachineDataAsync(
        //    long machineId,
        //    bool withCollections = false,
        //    DateTime? fromDateTime = null,
        //    DateTime? toDateTime = null)
        //{
        //    using (var connectionFactory = new DbConnectionFactory(ConnectionString))
        //    {
        //        // Get the machine
        //        var machineRepository = new MachineRepository(connectionFactory);
        //        var machine = await machineRepository.GetMachineAsync(machineId);
        //        if (machine == null)
        //        {
        //            return null;
        //        }

        //        if (withCollections)
        //        {
        //            // Get the collections
        //            var collectionRepository = new CollectionEventRepository(connectionFactory);
        //            machine.CollectionEvents = await collectionRepository.GetCollectionEventsForMachineAsync(machineId, fromDateTime, toDateTime);
        //        }
        //        else
        //        {
        //            machine.CollectionEvents = new List<CollectionEvent>();
        //        }

        //        return machine;
        //    }
        //}
    }
}
