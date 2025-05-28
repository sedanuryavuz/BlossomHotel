using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.WebUI.Dtos.HotelDto;

namespace BlossomHotel.WebUI.Dtos.RoomDto
{
    public class SearchAvailableRoomsDto
    {
        public int RoomId { get; set; }
        public string? RoomNumber { get; set; }
        public string? RoomTitle { get; set; }
        public string? RoomCoverImage { get; set; }
        public string? BedCount { get; set; }
        public int Price { get; set; }
        public int HotelId { get; set; }
        public ResultHotelDto? Hotel { get; set; }

    }
}
