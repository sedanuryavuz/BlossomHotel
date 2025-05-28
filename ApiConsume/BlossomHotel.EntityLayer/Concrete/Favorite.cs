using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.EntityLayer.Concrete
{
    public class Favorite
    {
        public int FavoriteId { get; set; }
        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
