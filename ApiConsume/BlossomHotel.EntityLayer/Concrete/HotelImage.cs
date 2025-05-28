using System.Text.Json.Serialization;

namespace BlossomHotel.EntityLayer.Concrete
{
    public class HotelImage
    {
        public int HotelImageId { get; set; }

        public int HotelId { get; set; }
        [JsonIgnore]
        public Hotel? Hotel { get; set; }

        public string? ImageUrl { get; set; }
    }
}