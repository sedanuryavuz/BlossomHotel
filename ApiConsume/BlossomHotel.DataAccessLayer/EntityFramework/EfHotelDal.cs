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
    public class EfHotelDal:GenericRepository<Hotel>, IHotelDal
    {
        private readonly Context _context;
        public EfHotelDal(Context context) : base(context)
        {
            _context = context;
        }

        public List<Hotel> GetHotelsWithGallery()
        {
            return _context.Hotels!
            .Include(h => h.Gallery)
            .ToList();
        }
    }
}
