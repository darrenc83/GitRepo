using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Core.Models
{
    [Table("Sellers")]
    public class SellerModel
    {
        public int Id { get; set; }
        public string SellersName { get; set; }
    }
}