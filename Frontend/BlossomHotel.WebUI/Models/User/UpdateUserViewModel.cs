using System.ComponentModel.DataAnnotations;

namespace BlossomHotel.WebUI.Models.User
{
    public class UpdateUserViewModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Surname { get; set; }
        public IFormFile? ProfilePhoto { get; set; }

        public string? ExistingPhotoPath { get; set; }

    }
}
