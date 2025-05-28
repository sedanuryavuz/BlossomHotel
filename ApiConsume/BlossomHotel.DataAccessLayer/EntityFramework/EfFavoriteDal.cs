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
    public class EfFavoriteDal : GenericRepository<Favorite>, IFavoriteDal
    {
        private readonly Context _context;
        public EfFavoriteDal(Context context) : base(context)
        {
            _context = context;
        }

        public List<Favorite> GetFavoritesByUserId(int userId)
        {
            //return _context.Set<Favorite>().Where(f => f.AppUserId == userId).ToList();
            return _context.Favorites
               .Where(f => f.AppUserId == userId)
               .Include(f => f.Room)
               .ThenInclude(r => r.Hotel)
               .ToList();
        }

    }
}
