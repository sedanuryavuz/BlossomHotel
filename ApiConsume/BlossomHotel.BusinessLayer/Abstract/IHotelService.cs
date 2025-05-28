using BlossomHotel.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.BusinessLayer.Abstract
{
    public interface IHotelService:IGenericService<Hotel>
    {
        List<Hotel> TGetHotelsWithGallery();
    }
}
