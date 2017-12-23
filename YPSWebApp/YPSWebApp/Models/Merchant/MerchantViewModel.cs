using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YPSWebApp.Models.AcquiringBank;
using YPSWebApp.Models.MerchantTerminal;
using YPSWebApp.Models.Notes;
using YPSWebApp.Models.Seller;
using YPSWebApp.Models.Terminal;

namespace YPSWebApp.Models.Merchant
{
    public class MerchantViewModel
    {

        public long Id { get; set; }

        [Required]
        [Key]
        [Display(Name = "Merchant Id")]
        public long MerchantId { get; set; }

        [Required]

        [Display(Name = "Merchant Name")]
        public string MerchantName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date")]
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Updated Date")]
        public DateTime? LastUpdatedDate { get; set; }

        [Required]
        [Display(Name = "Last Updated By")]
        public string LastUpdatedBy { get; set; }

        public AcquiringBankViewModel AcquiringBankViewModel { get; set; }
        public List< AcquiringBankViewModel> AcquiringBankViewModelList { get; set; }

        public SellerViewModel SellerViewModel { get; set; }
        public List<SellerViewModel> SellerViewModelList { get; set; }

        public NotesViewModel NotesViewModel { get; set; }
        public List<NotesViewModel> NotesViewModelList { get; set; }

        //public TerminalViewModel TerminalViewModel { get; set; }
      
        public MerchantTerminalViewModel MerchantTerminalViewModel { get; set; }





    }

}