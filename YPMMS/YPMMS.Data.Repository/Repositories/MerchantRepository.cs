using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using YPMMS.Data.Repository.Interfaces;
using YPMMS.Data.Repository.Models;
using YPMMS.Shared.Core.Models;
using System;

namespace YPMMS.Data.Repository.Repositories
{
    /// <summary>
    /// Repository for getting <see cref="Machine"/> data from the database
    /// </summary>
    public sealed class MerchantRepository : BaseRepository
    {
        public MerchantRepository(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }

        public async Task<Merchant> GetMerchantAsync(long merchantId)
        {
           


            var selectStatement = $"SELECT * FROM Merchant m " +
                    "left join Sellers s on m.SellerId=s.Id " +
                    "left join AcquiringBank ab on m.AcquiringBankId= ab.Id  where m.Id = @Id";

            var result = await Connection.QueryAsync<Merchant>(selectStatement, new { Id = merchantId });

            return result.SingleOrDefault();



           // var merchant = await Connection.QueryAsync(selectStatement);
            //return merchant;

        }

        
       

        /// <summary>
        /// Add a new machine to the database
        /// </summary>
        /// <param name="machine"><see cref="Machine"/> object to add to the database.</param>
        //public async Task AddMachineAsync(Machine machine)
        //{
        //    var insertStatement = RepositoryTools.BuildInsertStatement(
        //        nameof(Machine),
        //        nameof(Machine.Id),
        //        nameof(Machine.Name),
        //        nameof(Machine.SiteId),
        //        nameof(Machine.SystemType),
        //        nameof(Machine.Status),
        //        nameof(Machine.Account));
        //    // (other fields just pick up default values)

        //    var machineInsertModel = Mapper.Map<MachineInsertModel>(machine);

        //    Log.Trace(
        //        "Adding machine (Id={0}, Name={1}, SiteId={2}, Status={3}, SystemType={4}): {5}",
        //        machineInsertModel.Id, machineInsertModel.Name, machineInsertModel.SiteId,
        //        machineInsertModel.Status, machineInsertModel.SystemType, machineInsertModel.Account,
        //        insertStatement);

        //    await Connection.ExecuteAsync(insertStatement, machineInsertModel);
        //}

        /// <summary>
        /// Edit an existing machine in the database
        /// </summary>
        /// <param name="machine"><see cref="Machine"/> object to update in the database.</param>
        //public async Task EditMachineAsync(Machine machine)
        //{
        //    var updateStatement =
        //        $"update {nameof(Machine)} " +
        //        $"set {nameof(Machine.Name)} = @{nameof(Machine.Name)}, " +
        //        $"{nameof(Machine.SiteId)} = @{nameof(Machine.SiteId)} " +
        //        $"where {nameof(Machine.Id)} = @{nameof(Machine.Id)}";

        //    // Note: no need to map machine to a MachineInsertModel because we're not editing
        //    // any of the enum fields

        //    Log.Trace(
        //        "Editing machine (Id={0}, Name={1}, SiteId={2}): {3}",
        //        machine.Id, machine.Name, machine.SiteId,
        //        updateStatement);

        //    await Connection.ExecuteAsync(updateStatement, machine);
        //}


        /// <summary>
        /// Get the current overall machine totals for today
        /// </summary>
        /// <returns></returns>
        //public async Task<OverallTotal> GetTotalsAsync(long managerId = 0, long account = 0)
        //{
        //    var sql = "";
        //    if (account > 0)
        //    {
        //       sql = "select  SUM(Totalvalue) as valueCollected,Count(id) as numberOfCollection from CollectionEventWithTotalValue WHERE  Account=@account AND CollectionType='Collection' AND CAST(Timestamp AS DATE) = CAST(GETDATE() AS DATE)";
        //    }else
        //    {
        //        sql = "select  SUM(Totalvalue) as valueCollected,Count(id) as numberOfCollection from CollectionEventWithTotalValue WHERE  CollectionType='Collection' AND CAST(Timestamp AS DATE) = CAST(GETDATE() AS DATE)";
        //    }

        //    var result = await Connection.QueryAsync<OverallTotal>(sql, new
        //    {
        //        account = account,
        //    });

        //    return result.FirstOrDefault();

        //}


        /// <summary>
        /// Retrieve all machines with current cash values
        /// </summary>
        /// <returns>List of <see cref="Machine"/> objects</returns>
        public async Task<IList<Merchant>> GetMerchantsAsync()
        {

            var selectStatement =
                $"SELECT * FROM Merchant m " +
                    "left join Sellers s on m.SellerId=s.Id " +
                    "left join AcquiringBank ab on m.AcquiringBankId= ab.Id";


            var merchants = (await Connection.QueryAsync<Merchant, Seller, AcquiringBank, Merchant>(
                selectStatement,
                (merchant, seller, acquiringbank) =>
                {
                    merchant.Seller = seller;
                    merchant.AcquiringBank = acquiringbank;
                    return merchant;
                })).ToList();

            Log.Trace("Returning {0} merchant(s)", merchants.Count);

            return merchants;
        }

        public async Task<bool> DoesMerchantExistAsync(long merchantId)
        {
            var selectStatement = $"select count(*) from {nameof(Merchant)} where {nameof(Merchant.MerchantId)} = @{nameof(Merchant.MerchantId)}";

            Log.Trace("Checking if machine ID {0} exists: {1}", merchantId, selectStatement);

            var selectParams = new
            {
                MerchantId = merchantId
            };

            var result = await Connection.QueryAsync<int>(selectStatement, selectParams);
            var idExists = result.Single() > 0;

            Log.Trace("Returning {0}", idExists);

            return idExists;
        }

        public async Task<bool> DoesMachineNameExistAsync(string merchantName, long? ignoreMerchantId)
        {
            var selectStatement = $"select count(*) from {nameof(Merchant)} where {nameof(Merchant.MerchantName)} = @{nameof(Merchant.MerchantName)}";

            if (ignoreMerchantId == null)
            {
                Log.Trace("Checking if machine name {0} exists: {1}", merchantName, selectStatement);
            }
            else
            {
                selectStatement += $" and {nameof(Merchant.MerchantId)} != @{nameof(Merchant.MerchantId)}";
                Log.Trace("Checking if machine name {0} exists, excluding ID {1}: {2}", merchantName, ignoreMerchantId, selectStatement);
            }

            var selectParams = new
            {
                MerchantId = ignoreMerchantId,
                MerchantName = merchantName
            };

            var result = await Connection.QueryAsync<int>(selectStatement, selectParams);
            var nameExists = result.Single() > 0;

            Log.Trace("Returning {0}", nameExists);

            return nameExists;
        }

        /// <summary>
        /// Retrieve status counts for all machines
        /// </summary>
        /// <returns>Statuses of all machines</returns>
        //public async Task<MachineStatuses> GetMachineStatusesAsync(long account)
        //{
        //    var selectStatement =
        //        $"select {nameof(Machine.Status)}, {nameof(Machine.UpdateAvailable)} ";
        //    if (account > 0)
        //    {
        //       selectStatement +=  $"from {nameof(Machine)} where Account=@account";
        //    }else
        //    {
        //        selectStatement += $"from {nameof(Machine)} ";
        //    }

        //    Log.Trace("Getting machine statuses: {0}", selectStatement);

        //    var result = await Connection.QueryAsync<Machine>(selectStatement, new { account = account });
        //    var machines = result.ToList();

        //    var machineStatuses = new MachineStatuses
        //    {
        //        Statuses = machines.Select(m => m.Status).ToList(),
        //        UpdateCount = machines.Count(m => m.UpdateAvailable)
        //    };

        //    Log.Trace(
        //        "Returning statuses for {0} machines ({1} updates available)",
        //        machineStatuses.Statuses.Count,
        //        machineStatuses.UpdateCount);

        //    return machineStatuses;
        //}

        /// <summary>
        /// Retrieve a specific machine
        /// </summary>
        /// <param name="id">ID of the machine to retrieve</param>
        /// <returns><see cref="Machine"/> object, or null if the ID was not found in the database</returns>
        //public async Task<Machine> GetMachineAsync(long id)
        //{
        //    var selectStatement =
        //        $"select * from {Consts.DbViews.MachineWithCashValues} M " +
        //        $"join {nameof(Site)} S on M.{nameof(Machine.SiteId)} = S.{nameof(Site.Id)} " +
        //        $"where M.{nameof(Machine.Id)} = @{nameof(Machine.Id)}";

        //    Log.Trace("Getting machine (Id={0}): {1}", id, selectStatement);

        //    var selectParameters = new
        //    {
        //        Id = id
        //    };

        //    var machineResult = await Connection.QueryAsync<Machine, Site, Machine>(
        //        selectStatement,
        //        (machine, site) =>
        //        {
        //            machine.Site = site;
        //            return machine;
        //        },
        //        selectParameters);

        //    var retrievedMachine = machineResult.SingleOrDefault();
        //    if (retrievedMachine == null)
        //    {
        //        Log.Warn("No machine found with Id {0}", id);
        //        return null;
        //    }

        //    var selectStatements =
        //        $"select * from {nameof(MachineManifest)} " +
        //        $"where {nameof(MachineManifest.MachineId)} = @{nameof(Machine.Id)}\n" +
        //        $"select * from {nameof(MachineDenomination)} " +
        //        $"where {nameof(MachineDenomination.MachineId)} = @{nameof(Machine.Id)}\n";

        //    Log.Trace("Getting manifests and denominations for machine (Id={0}): {1}", id, selectStatements);

        //    var result = await Connection.QueryMultipleAsync(selectStatements, selectParameters);

        //    var manifestsResult = await result.ReadAsync<MachineManifest>();
        //    retrievedMachine.Manifests = manifestsResult.ToList();

        //    var denominationsResult = await result.ReadAsync<MachineDenomination>();
        //    retrievedMachine.Denominations = denominationsResult.ToList();

        //    Log.Trace(
        //        "Returning machine (Id={0}, Name={1}) with {2} manifests and {3} denominations",
        //        retrievedMachine.Id,
        //        retrievedMachine.Name,
        //        retrievedMachine.Manifests.Count,
        //        retrievedMachine.Denominations.Count);

        //    return retrievedMachine;
        //}

        //public async Task<IList<MachineSite>> GetMachinesWithSitesAsync()
        //{
        //    Log.Trace("Getting all Machines with Sites");

        //    var selectStatement =
        //        $"select M.{nameof(Machine.Id)}, M.{nameof(Machine.Name)}, S.* from Machine M " +
        //        $"join {nameof(Site)} S on M.{nameof(Machine.SiteId)} = S.{nameof(Site.Id)} ";

        //    var machines = (await Connection.QueryAsync<MachineSite, Site, MachineSite>(
        //        selectStatement,
        //        (machine, site) =>
        //        {
        //            machine.Site = site;
        //            return machine;
        //        })).ToList();

        //    Log.Trace("Returning {0} machine(s) with Sites", machines.Count);

        //    return machines;
        //}

        // public async Task<>

        /// <summary>
        /// Check if a proposed new machine ID already exists in the database
        /// </summary>
        /// <param name="machineId">ID to check</param>
        //public async Task<bool> DoesMachineIdExistAsync(long machineId)
        //{
        //    var selectStatement = $"select count(*) from {nameof(Machine)} where {nameof(Machine.Id)} = @{nameof(Machine.Id)}";

        //    Log.Trace("Checking if machine ID {0} exists: {1}", machineId, selectStatement);

        //    var selectParams = new
        //    {
        //        Id = machineId
        //    };

        //    var result = await Connection.QueryAsync<int>(selectStatement, selectParams);
        //    var idExists = result.Single() > 0;

        //    Log.Trace("Returning {0}", idExists);

        //    return idExists;
        //}

        /// <summary>
        /// Check if a proposed new machine name already exists in the database
        /// </summary>
        /// <param name="machineName">Name to check</param>
        /// <param name="ignoreMachineId">ID of machineto ignore when checking for duplicates</param>
        //public async Task<bool> DoesMachineNameExistAsync(string machineName, long? ignoreMachineId)
        //{
        //    var selectStatement = $"select count(*) from {nameof(Machine)} where {nameof(Machine.Name)} = @{nameof(Machine.Name)}";

        //    if (ignoreMachineId == null)
        //    {
        //        Log.Trace("Checking if machine name {0} exists: {1}", machineName, selectStatement);
        //    }
        //    else
        //    {
        //        selectStatement += $" and {nameof(Machine.Id)} != @{nameof(Machine.Id)}";
        //        Log.Trace("Checking if machine name {0} exists, excluding ID {1}: {2}", machineName, ignoreMachineId, selectStatement);
        //    }

        //    var selectParams = new
        //    {
        //        Id = ignoreMachineId,
        //        Name = machineName
        //    };

        //    var result = await Connection.QueryAsync<int>(selectStatement, selectParams);
        //    var nameExists = result.Single() > 0;

        //    Log.Trace("Returning {0}", nameExists);

        //    return nameExists;
        //}

        /// <summary>
        /// Delete one or more machine from the database
        /// </summary>
        /// <param name="machineIds">List of machine IDs. If null or empty, this method is a no-op</param>
        /// <returns>Machines that were deleted</returns>
        //public async Task<IList<Machine>> DeleteMachinesAsync(IList<long> machineIds)
        //{
        //    if (machineIds == null || !machineIds.Any())
        //    {
        //        return new List<Machine>();
        //    }

        //    // Get the machines we're going to delete
        //    var deletedMachines = await GetMachinesAsync(machineIds);

        //    var deleteStatement = $"delete {nameof(Machine)} where {nameof(Machine.Id)} in @MachineIds";

        //    Log.Trace("Deleting machines with IDs [{0}]", string.Join(",", machineIds));

        //    var deleteParams = new
        //    {
        //        MachineIds = machineIds
        //    };
        //    await Connection.ExecuteAsync(deleteStatement, deleteParams);

        //    return deletedMachines;
        //}
    }
}
