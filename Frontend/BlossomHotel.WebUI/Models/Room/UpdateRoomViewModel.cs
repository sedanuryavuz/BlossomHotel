using BlossomHotel.WebUI.Dtos.HotelDto;
using BlossomHotel.WebUI.Dtos.RoomDto;

namespace BlossomHotel.WebUI.Models.Room
{
    public class UpdateRoomViewModel
    {
        public UpdateRoomDto? UpdateRoomDto { get; set; }
        public List<ResultHotelDto>? Hotels { get; set; }
    }
}