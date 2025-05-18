namespace BlossomHotel.WebUI.Dtos.HotelDto
{
    public class UpdateHotelDto
    {
        public int HotelId { get; set; }
        public string? HotelName { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
    }
}
