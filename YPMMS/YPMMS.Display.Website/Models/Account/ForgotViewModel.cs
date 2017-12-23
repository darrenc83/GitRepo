using System.ComponentModel.DataAnnotations;

namespace YPMMS.Display.Website.Models.Account
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}