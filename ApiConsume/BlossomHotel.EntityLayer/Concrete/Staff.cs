using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.EntityLayer.Concrete
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Fullname => $"{Name} {Surname}";
        public string? Gender { get; set; }
        public string? Description { get; set; }
        public string? WorkDepartment { get; set; }
        public string? ImageUrl { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
