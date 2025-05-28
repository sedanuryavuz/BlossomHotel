using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.EntityLayer.Concrete
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string? Name { get; set; }
        public string? Mail { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime CheckOut { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int RoomCount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? SpecialRequest { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public decimal TotalPrice { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
