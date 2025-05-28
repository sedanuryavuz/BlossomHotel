using BlossomHotel.WebUI.Dtos.RoomDto;
using System.ComponentModel.DataAnnotations;

namespace BlossomHotel.WebUI.Models.Booking
{
    public class CreateBookingViewModel
    {
        public int RoomId { get; set; }
        public string? RoomName { get; set; }

        public int AppUserId { get; set; }
        public string? Name { get; set; }
        public string? Mail { get; set; }

        public DateTime Checkin { get; set; }
        public DateTime CheckOut { get; set; }

        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int RoomCount { get; set; }

        public string? SpecialRequest { get; set; }
        public string? Description { get; set; }
        public decimal RoomPrice { get; set; }

    }
}
