using BlossomHotel.EntityLayer.Concrete;

namespace BlossomHotel.WebUI.Dtos.RoomDto
{
    public class UpdateRoomDto
    {
        public int RoomId { get; set; }
        public string? RoomNumber { get; set; }
        public string? RoomTitle { get; set; }
        //public string? RoomCoverImage { get; set; }
        public string? BedCount { get; set; }
        public string? BathCount { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public IFormFile? CoverImage { get; set; }
        public List<IFormFile>? NewRoomImages { get; set; }
        public string? RoomCoverImageUrl { get; set; }
        public List<string>? RoomImageUrls { get; set; }
        public ICollection<RoomImage>? RoomGallery { get; set; }


    }

}
