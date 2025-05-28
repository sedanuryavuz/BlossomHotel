using BlossomHotel.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.BusinessLayer.Abstract
{
    public interface IFavoriteService:IGenericService<Favorite>
    {
        List<Favorite> TGetFavoritesByUserId(int userId);
    }
}
