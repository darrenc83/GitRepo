using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Shared.Core.Models
{
    [Table("Merchants")]
    public class MerchantModel
    {
        
        public long Id { get; set; }
        
        public long MerchantId { get; set; }     
        public string MerchantName { get; set; }       
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }        
        public DateTime? LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public long AcquiringBankId { get; set; }
        public long SellerId { get; set; }

        public AcquiringBankModel AcquiringBankModel { get; set; }

        public SellerModel SellerModel { get; set; }

        //public MerchantTerminalModel MerchantTerminalsModel { get; set; }

       // public MerchantNotesModel MerchantNotesModel { get; set; }

        public virtual ICollection<NotesModel> Notes { get; set; }

        public virtual ICollection<TerminalModel> Terminals { get; set; }

    }


}
