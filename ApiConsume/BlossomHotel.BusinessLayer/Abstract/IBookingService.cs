using BlossomHotel.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.BusinessLayer.Abstract
{
    public interface IBookingService:IGenericService<Booking>
    {
        List<Booking> TGetBookingsByUserId(int userId);
        void TBookingStatusChangeApproved(int id);
        void TBookingStatusChangeCancel(int id);
        void TBookingStatusChangeWait(int id);
        List<Booking> TGetBookingsByRoomId(int roomId);
        decimal TGetTotalRevenue();
        List<Booking> TGetRecentBookings(int count);
        List<Booking> TGetUpcomingCheckOuts(int count);
    }
}
