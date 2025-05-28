using BlossomHotel.DataAccessLayer.Abstract;
using BlossomHotel.DataAccessLayer.Concrete;
using BlossomHotel.DataAccessLayer.Repositories;
using BlossomHotel.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.DataAccessLayer.EntityFramework
{
    public class EfRoomDal : GenericRepository<Room>, IRoomDal
    {
        private readonly Context _context;
        public EfRoomDal(Context context) : base(context)
        {
            _context = context;
        }

        public void DeleteRoomImage(RoomImage image)
        {
            _context.RoomImages.Remove(image);
            _context.SaveChanges();
        }

        public List<Room> GetAvailableRooms(int hotelId, DateTime checkin, DateTime checkout)
        {
          
            return _context.Rooms!
                .Include(r => r.Bookings)
                .Include(r => r.Hotel)
                .Where(r => r.HotelId == hotelId &&
                            !r.Bookings!.Any(b =>
                                b.Checkin < checkout &&
                                b.CheckOut > checkin))
                .ToList();
        }

        public int GetOccupiedRoomCount()
        {
            var today = DateTime.Today;

            var occupiedRoomIds = _context.Bookings
                .Where(b => b.Checkin <= today && b.CheckOut >= today)
                .Select(b => b.RoomId)
                .Distinct()
                .ToList();

            return occupiedRoomIds.Count;
        }

        public RoomImage GetRoomImageByUrl(string url)
        {
            return _context.RoomImages.FirstOrDefault(x => x.ImageUrl == url);
            //return _context.RoomImages.FirstOrDefault(x => x.ImageUrl.EndsWith(url));
            //return _context.RoomImages.FirstOrDefault(x => x.ImageUrl == url || x.ImageUrl == "/" + url);

        }

        public Room GetRoomWithImages(int id)
        {
            return _context.Rooms
       .Include(r => r.RoomGallery)
       .FirstOrDefault(r => r.RoomId == id);
        }

        public int GetTotalRoomCount()
        {
            return _context.Rooms.Count();
        }
    }
}
