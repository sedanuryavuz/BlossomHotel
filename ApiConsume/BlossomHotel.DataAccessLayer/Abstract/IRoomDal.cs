using BlossomHotel.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.DataAccessLayer.Abstract
{
    public interface IRoomDal : IGenericDal<Room>
    {
        List<Room> GetAvailableRooms(int hotelId, DateTime checkin, DateTime checkout);
        Room GetRoomWithImages(int id);
        RoomImage GetRoomImageByUrl(string url);
        void DeleteRoomImage(RoomImage image);
        int GetTotalRoomCount();
        int GetOccupiedRoomCount();
    }
}
