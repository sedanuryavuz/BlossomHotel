using System.ComponentModel.DataAnnotations;

namespace BlossomHotel.WebUI.Models.User
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Eski şifre zorunludur.")]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }

        [Required(ErrorMessage = "Yeni şifre zorunludur.")]
        [StringLength(100, ErrorMessage = "{0} en az {2} karakter olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Yeni şifre tekrar zorunludur.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Yeni şifreler eşleşmiyor.")]
        public string? ConfirmPassword { get; set; }
    }
}
