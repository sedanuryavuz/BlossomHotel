namespace BlossomHotel.WebUI.Dtos.AboutDto
{
    public class UpdateAboutDto
    {
        public int AboutId { get; set; }

        public string? Title1 { get; set; }
        public string? Title2 { get; set; }
        public string? SocialPlatform { get; set; }

        public string? Url { get; set; }

        public string? IconClass { get; set; }
        public string? Description1 { get; set; }
        public string? Description2 { get; set; }
        public int StaffCount { get; set; }
        public int RoomCount { get; set; }
        public int CustomerCount { get; set; }
    }
}
