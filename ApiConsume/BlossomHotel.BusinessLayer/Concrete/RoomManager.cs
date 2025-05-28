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
    public class RoomManager : IRoomService
    {
        private readonly IRoomDal _roomDal;

        public RoomManager(IRoomDal roomDal)
        {
            _roomDal = roomDal;
        }

        public List<Room> TGetAvailableRooms(int hotelId, DateTime checkin, DateTime checkout)
        {
            return _roomDal.GetAvailableRooms(hotelId, checkin, checkout);
        }

        public void TDelete(Room t)
        {
            _roomDal.Delete(t);
        }

        public void TDeleteRoomImage(RoomImage image)
        {
            _roomDal.DeleteRoomImage(image);
        }

        public Room TGetById(int id)
        {
            return _roomDal.GetById(id);
        }

        public List<Room> TGetList()
        {
            return _roomDal.GetList();
        }

        public RoomImage TGetRoomImageByUrl(string url)
        {
            return _roomDal.GetRoomImageByUrl(url);
        }

        public Room TGetRoomWithImages(int id)
        {
            return _roomDal.GetRoomWithImages(id);
        }

        public void TInsert(Room t)
        {
            _roomDal.Insert(t);
        }

        public void TUpdate(Room t)
        {
            _roomDal.Update(t);
        }

        public int TGetTotalRoomCount()
        {
            return _roomDal.GetTotalRoomCount();
        }

        public int TGetOccupiedRoomCount()
        {
            return _roomDal.GetOccupiedRoomCount();
        }
    }
}
