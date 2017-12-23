using System.ComponentModel.DataAnnotations;

namespace YPMMS.Display.Website.Models.ManageAccount
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}