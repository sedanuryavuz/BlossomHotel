namespace BlossomHotel.WebUI.Models.Room
{
    public class SearchRoomViewModel
    {
        public int HotelId { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
    }
}
