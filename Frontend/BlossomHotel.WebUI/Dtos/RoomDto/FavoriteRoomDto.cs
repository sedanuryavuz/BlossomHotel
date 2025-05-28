using BlossomHotel.EntityLayer.Concrete;

namespace BlossomHotel.WebUI.Dtos.RoomDto
{
    public class FavoriteRoomDto
    {
        public int FavoriteId { get; set; }
        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
