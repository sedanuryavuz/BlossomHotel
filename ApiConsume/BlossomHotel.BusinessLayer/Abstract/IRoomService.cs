using BlossomHotel.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.BusinessLayer.Abstract
{
    public interface IRoomService:IGenericService<Room>
    {
        List<Room> TGetAvailableRooms(int hotelId, DateTime checkin, DateTime checkout);
        Room TGetRoomWithImages(int id);
        RoomImage TGetRoomImageByUrl(string url);
        void TDeleteRoomImage(RoomImage image);
        int TGetTotalRoomCount();
        int TGetOccupiedRoomCount();
    }
}
