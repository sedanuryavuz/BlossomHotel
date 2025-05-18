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
    public class HotelManager : IHotelService
    {
        private readonly IHotelDal _hotelDal;

        public HotelManager(IHotelDal hotelDal)
        {
            _hotelDal = hotelDal;
        }

        public void TDelete(Hotel t)
        {
            _hotelDal.Delete(t);
        }

        public Hotel TGetById(int id)
        {
            return _hotelDal.GetById(id);
        }

        public List<Hotel> TGetList()
        {
            return _hotelDal.GetList();
        }

        public void TInsert(Hotel t)
        {
            _hotelDal.Insert(t);
        }

        public void TUpdate(Hotel t)
        {
            _hotelDal.Update(t);
        }
    }
}
