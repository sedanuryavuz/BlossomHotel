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
    public class FavoriteManager : IFavoriteService
    {
        private readonly IFavoriteDal _favoriteDal;

        public FavoriteManager(IFavoriteDal favoriteDal)
        {
            _favoriteDal = favoriteDal;
        }

        public List<Favorite> TGetFavoritesByUserId(int userId)
        {
            return _favoriteDal.GetFavoritesByUserId(userId);
        }

        public void TDelete(Favorite t)
        {
            _favoriteDal.Delete(t);
        }

        public Favorite TGetById(int id)
        {
            return _favoriteDal.GetById(id);
        }

        public List<Favorite> TGetList()
        {
            return _favoriteDal.GetList();
        }

        public void TInsert(Favorite t)
        {
            _favoriteDal.Insert(t);
        }

        public void TUpdate(Favorite t)
        {
            _favoriteDal.Update(t);
        }
    }
}
