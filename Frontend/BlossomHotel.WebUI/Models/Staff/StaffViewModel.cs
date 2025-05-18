using BlossomHotel.EntityLayer.Concrete;

namespace BlossomHotel.WebUI.Models.Staff
{
    public class StaffViewModel
    {
        public int StaffId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Fullname => $"{Name} {Surname}";
        public string? WorkDepartment { get; set; }
        public int HotelIdId { get; set; }
        public Hotel? Hotel { get; set; }

    }
}