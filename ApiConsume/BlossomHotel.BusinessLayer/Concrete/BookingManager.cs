using BlossomHotel.BusinessLayer.Abstract;
using BlossomHotel.DataAccessLayer.Abstract;
using BlossomHotel.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.BusinessLayer.Concrete
{
    public class BookingManager : IBookingService
    {
        private readonly IBookingDal _bookingDal;
        public BookingManager(IBookingDal bookingDal)
        {
            _bookingDal = bookingDal;
        }

        public List<Booking> TGetBookingsByUserId(int userId)
        {
            return _bookingDal.GetBookingsByUserId(userId);
        }

        public void TBookingStatusChangeApproved(int id)
        {
            _bookingDal.BookingStatusChangeApproved(id);
        }

        public void TBookingStatusChangeCancel(int id)
        {
            _bookingDal.BookingStatusChangeCancel(id);
        }

        public void TBookingStatusChangeWait(int id)
        {
            _bookingDal.BookingStatusChangeWait(id);
        }

        public void TDelete(Booking t)
        {
            _bookingDal.Delete(t);
        }

        public Booking TGetById(int id)
        {
            return _bookingDal.GetById(id);
        }

        public List<Booking> TGetList()
        {
            return _bookingDal.GetList();
        }

        public void TInsert(Booking t)
        {
            _bookingDal.Insert(t);
        }

        public void TUpdate(Booking t)
        {
            _bookingDal.Update(t);
        }

        public List<Booking> TGetBookingsByRoomId(int roomId)
        {
            return _bookingDal.GetBookingsByRoomId(roomId);
        }

        public decimal TGetTotalRevenue()
        {
            return _bookingDal.GetTotalRevenue();
        }

        public List<Booking> TGetRecentBookings(int count)
        {
            return _bookingDal.GetRecentBookings(count);
        }

        public List<Booking> TGetUpcomingCheckOuts(int count)
        {
            return _bookingDal.GetUpcomingCheckOuts(count);
        }
    }
}
