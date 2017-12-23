using System;
using System.Collections.Generic;
using System.Linq;
using YPMMS.Shared.Core.Enums;
using YPMMS.Shared.Core.Models;

namespace YPMMS.Display.Website.Models.Overview
{
    public sealed class OverviewMerchantsJsonModel
    {
        /// <summary>
        /// The set of machines accessible to the current user
        /// </summary>
        public IList<Merchant> Merchants { get; set; }

        ///// <summary>
        ///// Total number of offline machines
        ///// </summary>
        //public int OfflineCount => Machines.Count(m => m.Status == SystemStatus.Offline);

        ///// <summary>
        ///// Total number of full machines
        ///// </summary>
        //public int FullCount => Machines.Count(m => m.Status.IsFull());

        ///// <summary>
        ///// Total number of machines with updates available
        ///// </summary>
        //public int UpdateCount => Machines.Count(m => m.UpdateAvailable);

        ///// <summary>
        /////  Total number of machines with an error status
        ///// </summary>
        //public int ErrorCount => Machines.Count(m => m.Status.IsError());

        ///// <summary>
        ///// Total collectable value across all machines
        ///// </summary>
        //public decimal AllMachinesCollectableValue => Machines.Sum(m => m.CollectableValue);

        ///// <summary>
        ///// Number of machines that have been collected today
        ///// </summary>
        //public int CollectedTodayCount
        //    => Machines.Count(m => m.LastCollectionTime.GetValueOrDefault().Date.Equals(DateTime.UtcNow.Date));

        ///// <summary>
        ///// Total amount of cash collected today from all machines
        ///// </summary>
        //public decimal CollectedTodayValue => Machines
        //    .Where(m => m.LastCollectionTime.GetValueOrDefault().Date.Equals(DateTime.UtcNow.Date))
        //    .Sum(m => m.LastCollectionAmount.GetValueOrDefault());

        ///// <summary>
        ///// Currency for this set of machines
        ///// </summary>
        //public string Currency => (Machines.Any() ? Machines.First().Currency ?? string.Empty : string.Empty);
    }
}