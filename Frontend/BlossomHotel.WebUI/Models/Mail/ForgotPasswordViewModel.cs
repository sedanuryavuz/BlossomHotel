using System.ComponentModel.DataAnnotations;

namespace BlossomHotel.WebUI.Models.Mail
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
