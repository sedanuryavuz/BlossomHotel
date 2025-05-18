using System.ComponentModel.DataAnnotations;

namespace BlossomHotel.WebUI.Dtos.LoginDto
{
    public class LoginUserDto
    {
        [Required(ErrorMessage ="Kullanıcı adını giriniz.")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Şifreyi giriniz.")]
        public string? Password { get; set; }
    }
}
