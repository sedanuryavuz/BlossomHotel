using BlossomHotel.EntityLayer.Concrete;

namespace BlossomHotel.WebUI.Dtos.RoomDto
{
    public class ResultRoomDto
    {
        public int RoomId { get; set; }
        public string? RoomNumber { get; set; }
        public string? RoomTitle { get; set; }
        public string? RoomCoverImage { get; set; }
        // public ICollection<RoomImage>? RoomGallery { get; set; } 
        public List<IFormFile>? RoomGallery { get; set; }
        //public IFormFile? RoomCoverImage { get; set; }
        public string RoomCoverImageUrl { get; set; }
        public List<string> RoomImageUrls { get; set; }
        public string? BedCount { get; set; }
        public string? BathCount { get; set; }
        public string? Description { get; set; }
        public string? RoomType { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
        public string? HotelName { get; set; }
        public FavoriteRoomDto? Favorite { get; set; }

    }
}
