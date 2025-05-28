
using BlossomHotel.DataAccessLayer.Abstract;
using BlossomHotel.DataAccessLayer.Concrete;
using BlossomHotel.DataAccessLayer.Repositories;
using BlossomHotel.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace BlossomHotel.DataAccessLayer.EntityFramework
{
    public class EfBookingDal : GenericRepository<Booking>, IBookingDal
    {
        private readonly Context _context;
        public EfBookingDal(Context context) : base(context)
        {
            _context = context;
        }

        public void BookingStatusChangeApproved(int id)
        {
            var context = new Context();
            var values = context.Bookings?.Find(id);
            values!.Status = "Onaylandı";
            context.SaveChanges();
        }

        public void BookingStatusChangeCancel(int id)
        {
            var context = new Context();
            var values = _context.Bookings?.Find(id);
            values!.Status = "İptal Edildi";
            _context.SaveChanges();
        }

        public void BookingStatusChangeWait(int id)
        {
            var context = new Context();
            var values = context.Bookings?.Find(id);
            values!.Status = "Müşteri Aranacak";
            context.SaveChanges();
        }

        public List<Booking> GetBookingsByRoomId(int roomId)
        {
            return _context.Bookings
              .Include(b => b.Room)
              .ThenInclude(r => r.Hotel)
              .Where(b => b.RoomId == roomId)
              .ToList();

        }

        public List<Booking> GetBookingsByUserId(int userId)
        {
            return _context.Bookings
         .Include(b => b.Room)
             .ThenInclude(r => r.Hotel)
         .Where(b => b.AppUserId == userId)
         .ToList();
        }

        public List<Booking> GetRecentBookings(int count)
        {
            return _context.Bookings
           .Include(x => x.Room)
           .Include(x => x.AppUser)
           .OrderByDescending(x => x.CreatedAt)
           .Take(count)
           .ToList();
        }

        public decimal GetTotalRevenue()
        {
            return _context.Bookings
          .Where(x => x.Status == "CheckedIn" || x.Status == "Completed")
          .Sum(x => x.TotalPrice); 
        }

        public List<Booking> GetUpcomingCheckOuts(int count)
        {
            var today = DateTime.Today;
            return _context.Bookings
                .Where(x => x.CheckOut >= today)
                .OrderBy(x => x.CheckOut)
                .Take(count)
                .Include(x => x.Room)
                .Include(x => x.AppUser)
                .ToList();
        }
    }
}
