using BlossomHotel.WebUI.Dtos.HotelDto;
using BlossomHotel.WebUI.Dtos.RoomDto;

namespace BlossomHotel.WebUI.Models.Room
{
    public class CreateRoomViewModel
    {
        public CreateRoomDto? CreateRoomDto { get; set; }
        public List<ResultHotelDto>? Hotels { get; set; }
    }
}
