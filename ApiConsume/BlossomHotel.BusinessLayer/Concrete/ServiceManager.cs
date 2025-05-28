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
    public class ServiceManager : IServiceService
    {
        private readonly IServicesDal _serviceDal;
        public ServiceManager(IServicesDal servicesDal)
        {
            _serviceDal = servicesDal;
        }
        public void TDelete(Service t)
        {
            _serviceDal.Delete(t);
        }

        public Service TGetById(int id)
        {
            return _serviceDal.GetById(id);
        }

        public List<Service> TGetList()
        {
            return _serviceDal.GetList();
        }

        public void TInsert(Service t)
        {
            _serviceDal.Insert(t);
        }

        public void TUpdate(Service t)
        {
            _serviceDal.Update(t);
        }
    }
}
