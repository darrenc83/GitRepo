using System;
using System.Collections.Generic;
using System.Linq;
using YPMMS.Shared.Core.Enums;
using YPMMS.Shared.Core.Extensions;
using Newtonsoft.Json;

namespace YPMMS.Shared.Core.Models
{
    /// <summary>
    /// Model to represent a single machine
    /// </summary>
    public sealed class Merchant    {

       

            public long Id { get; set; }

            public long MerchantId { get; set; }
            public string MerchantName { get; set; }
            public DateTime CreatedDate { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? LastUpdatedDate { get; set; }
            public string LastUpdatedBy { get; set; }
            public long AcquiringBankId { get; set; }
            public long SellerId { get; set; }
            public DateTime? ApprovedDate { get; set; }
            public DateTime? WelcomeCallDate { get; set; }
            public DateTime? ApproxLiveDate { get; set; }

            public AcquiringBank AcquiringBank { get; set; }

            public Seller Seller{ get; set; }

            //public MerchantTerminalModel MerchantTerminalsModel { get; set; }

            // public MerchantNotesModel MerchantNotesModel { get; set; }

            //public virtual ICollection<NotesModel> Notes { get; set; }

            //public virtual ICollection<TerminalModel> Terminals { get; set; }

        



        //public long Id { get; set; }
        //public string Name { get; set; }
        //public string Currency { get; set; }
        //public SystemType SystemType { get; set; }
        //public SystemStatus Status { get; set; }
        //public string TcpUrl { get; set; }
        //public string TcpPort { get; set; }
        //public bool UpdateAvailable { get; set; }
        //public long SiteId { get; set; }
        //public long Account { get; set; }
        //public Site Site { get; set; }

        //[JsonIgnore]
        //public DateTime? LastConnectedTime { get; set; }

        //[JsonProperty("LastConnectedTime")]
        //public DateTime? LastConnectedTimeUtc => LastConnectedTime?.SpecifyUtc();

        //[JsonIgnore]
        //public DateTime? LastCollectionTime { get; set; }
        //[JsonProperty("LastCollectionTime")]
        //public DateTime? LastCollectionTimeUtc => LastCollectionTime?.SpecifyUtc();

        //public decimal? LastCollectionAmount { get; set; }

        //[JsonIgnore]
        //public DateTime Timestamp { get; set; }

        //[JsonProperty("Timestamp")]
        //public DateTime TimestampUtc => Timestamp.SpecifyUtc();

        //public decimal CashboxValue { get; set; }
        //public decimal StoredValue { get; set; }
        //public decimal FloatValue { get; set; }
        //public decimal CollectableValue { get; set; }

        //public IList<MachineManifest> Manifests { get; set; }
        //public IList<MachineDenomination> Denominations { get; set; }
        //public IList<CollectionEvent> CollectionEvents { get; set; }

        //public TebsCode CurrentBag { get; set; }

        //public TebsBag CurrentBagData { get; set; }


        //#region Calculated properties

        ///// <summary>
        ///// List of bill denominations supported by this machine
        ///// </summary>
        //public IEnumerable<MachineDenomination> BillDenominations
        //{
        //    get
        //    {
        //        return Denominations?.Where(d => d.Type == CashType.Bill) ?? new List<MachineDenomination>();
        //    }
        //}
        //public decimal BillsCashboxValue => BillDenominations.Sum(c => c.CashboxValue);
        //public decimal BillsStoredValue => BillDenominations.Sum(c => c.StoredValue);

        //public decimal BillsCollectableValue => BillDenominations.Sum(c => c.CollectableValue);
        //public decimal BillsTotalValue => BillDenominations.Sum(c => c.TotalValue);

        //public decimal TotalValue => CashboxValue + StoredValue;
        //public bool StatusIsFull => Status.IsFull();
        //public bool StatusIsError => Status.IsError();
        //public string StatusDescription => Status.GetDescription();



    }
}
