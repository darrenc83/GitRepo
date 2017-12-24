using System.Collections.Generic;
using YPSWebApp.Models.Terminal;

namespace YPSWebApp.Models.MerchantTerminal
{
    public class MerchantTerminalViewModel
    {
        public int MerchantId { get; set; }
        public int TerminalId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        //public MerchantModel Merchant { get; set; }
        public List<TerminalViewModel> MerchantTerminalViewModelList { get; set; }
    }
}
