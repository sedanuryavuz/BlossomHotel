using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.EntityLayer.Concrete
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string? HotelName { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? City { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public List<Room>? Rooms { get; set; }
        public List<Staff>? Staffs { get; set; }
        public ICollection<HotelImage>? Gallery { get; set; }
    }
}
