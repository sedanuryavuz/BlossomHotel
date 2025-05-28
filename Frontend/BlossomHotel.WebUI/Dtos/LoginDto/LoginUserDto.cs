using System.ComponentModel.DataAnnotations;

namespace BlossomHotel.WebUI.Dtos.LoginDto
{
    public class LoginUserDto
    {
        
        public string? Mail { get; set; }
        public string? Password { get; set; }
    }
}
