using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.EntityLayer.Concrete
{
    public class About
    {
        public int AboutId { get; set; }

        public string? Title1 { get; set; }
        public string? Title2 { get; set; }
        public string? SocialPlatform { get; set; }
        public string? SocialPlatform2 { get; set; }
        public string? SocialPlatform3 { get; set; }
        public string? SocialPlatform4 { get; set; }
        public string? IconClass { get; set; }
        public string? IconClass2 { get; set; }
        public string? IconClass3 { get; set; }
        public string? IconClass4 { get; set; }

        public string? Description1 { get; set; }
        public string? Description2 { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Mail { get; set; }
        public string? Address { get; set; }
        public int StaffCount { get; set; }
        public int RoomCount { get; set; }
        public int CustomerCount { get; set; }
    }
}
