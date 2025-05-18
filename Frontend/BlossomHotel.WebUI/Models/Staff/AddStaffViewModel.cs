using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.WebUI.Dtos.HotelDto;
using BlossomHotel.WebUI.Dtos.RoomDto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlossomHotel.WebUI.Models.Staff
{
    public class AddStaffViewModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Gender { get; set; }
        public string? WorkDepartment { get; set; }
        public string? ImageUrl { get; set; }
        public int HotelId { get; set; }
        public List<SelectListItem>? Hotels { get; set; }

    }
}
