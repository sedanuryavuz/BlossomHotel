using System.Text.Json.Serialization;

namespace BlossomHotel.EntityLayer.Concrete
{
    public class RoomImage
    {
        public int RoomImageId { get; set; }

        public int RoomId { get; set; }
        [JsonIgnore]
        public Room? Room { get; set; }

        public string? ImageUrl { get; set; }
    }
}