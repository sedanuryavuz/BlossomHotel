using BlossomHotel.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.DataAccessLayer.Abstract
{
    public interface IBookingDal:IGenericDal<Booking>
    {
        List<Booking> GetBookingsByUserId(int userId);
        void BookingStatusChangeApproved(int id);
        void BookingStatusChangeCancel(int id);
        void BookingStatusChangeWait(int id);
        List<Booking> GetBookingsByRoomId(int roomId);
        decimal GetTotalRevenue();
        List<Booking> GetRecentBookings(int count);
        List<Booking> GetUpcomingCheckOuts(int count);
    }
}
