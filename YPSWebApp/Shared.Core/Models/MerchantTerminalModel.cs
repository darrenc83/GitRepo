using System.Collections.Generic;

namespace Shared.Core.Models
{
    public class MerchantTerminalModel
    {
        public int MerchantId { get; set; }
        public int TerminalId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        //public MerchantModel Merchant { get; set; }
        public List<TerminalModel> MerchantTerminalModelList { get; set; }
    }
}
