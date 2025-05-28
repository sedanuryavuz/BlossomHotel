using BlossomHotel.DataAccessLayer.Abstract;
using BlossomHotel.DataAccessLayer.Concrete;
using BlossomHotel.DataAccessLayer.Repositories;
using BlossomHotel.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.DataAccessLayer.EntityFramework
{
    public class EfAboutDal : GenericRepository<About>, IAboutDal
    {
        public EfAboutDal(Context context) : base(context)
        {
        }
    }
}
