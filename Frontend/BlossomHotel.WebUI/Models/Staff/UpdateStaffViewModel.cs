using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.WebUI.Dtos.HotelDto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlossomHotel.WebUI.Models.Staff
{
    public class UpdateStaffViewModel
    {
        public int StaffId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Gender { get; set; }
        public string? WorkDepartment { get; set; }
        public string? ImageUrl { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public List<ResultHotelDto>? Hotels { get; set; }
    }
}
