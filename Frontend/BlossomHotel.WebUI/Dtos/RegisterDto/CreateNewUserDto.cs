using System.ComponentModel.DataAnnotations;

namespace BlossomHotel.WebUI.Dtos.RegisterDto
{
    public class CreateNewUserDto
    {
        public string? Name { get; set; }    
        public string? Surname { get; set; }     
        public string? Mail { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string? ConfirmPassword { get; set; }

    }
}
