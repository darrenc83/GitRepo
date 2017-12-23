using System.ComponentModel.DataAnnotations;

namespace YPSWebApp.Models.AcquiringBank
{
    public class AcquiringBankViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Acquiring Bank")]
        public string Bank { get; set; }

    }
}