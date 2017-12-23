using System;

namespace YPMMS.Data.Repository
{
    internal static class Consts
    {
        /// <summary>
        /// When retrieving a collector or machine with recent collections, return collections
        /// within this time span if none other specified.
        /// </summary>
        internal static readonly TimeSpan DefaultCollectionHistorySpan = TimeSpan.FromDays(30);

        /// <summary>
        /// Names of views defined in the database schema
        /// </summary>
        internal static class DbViews
        {
            public const string CollectionEventWithTotalValue = "CollectionEventWithTotalValue";
            public const string CollectorWithUserDetails = "CollectorWithUserDetails";
            public const string MachineWithCashValues = "MachineWithCashValues";
            public const string SystemEventWithDetails = "SystemEventWithDetails";
        }

        /// <summary>
        /// Names of tables that we can't derive from a C# class name
        /// </summary>
        internal static class DbTables
        {
            public const string CollectorToMachine = "CollectorToMachine";
        }

        /// <summary>
        /// Names of stored procedures defined in the database
        /// </summary>
        internal static class DbSprocs
        {
            public const string CreateSystemEvent = "CreateSystemEvent";
            public const string GetSystemEventsForMachine = "GetSystemEventsForMachine";
            public const string GetSystemEventsForUser = "GetSystemEventsForUser";
        }
    }
}