using System.ComponentModel.DataAnnotations;

namespace YPSWebApp.Models.Seller
{
    public class SellerViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Sellers Name")]
        public string SellersName { get; set; }
    }
}