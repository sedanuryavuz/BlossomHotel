namespace BlossomHotel.WebUI.Models.Room
{
    public class RoomViewModel
    {
        public int RoomId { get; set; }
        public string? RoomNumber { get; set; }
        public string? RoomTitle { get; set; }
        public string? RoomCoverImage { get; set; }
        public string? BedCount { get; set; }
        public string? BathCount { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }

    }
}
