using BlossomHotel.EntityLayer.Concrete;

namespace BlossomHotel.WebUI.Dtos.HotelDto
{
    public class ResultHotelWithGalleryDto
    {
        public int HotelId { get; set; }
        public string? HotelName { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? City { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<HotelImage>? Gallery { get; set; }
    }
}
