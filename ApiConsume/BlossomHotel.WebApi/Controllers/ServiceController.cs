using BlossomHotel.BusinessLayer.Abstract;
using BlossomHotel.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossomHotel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        [HttpGet]
        public IActionResult ServiceList()
        {
            var values = _serviceService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult ServiceAdd(Service service)
        {
            _serviceService.TInsert(service);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult ServiceDelete(int id)
        {
            var service = _serviceService.TGetById(id);
            _serviceService.TDelete(service);
            return Ok();
        }
        [HttpPut]
        public IActionResult ServiceUpdate(Service service)
        {
            _serviceService.TUpdate(service);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult ServiceGetById(int id)
        {
            var service = _serviceService.TGetById(id);
            return Ok(service);
        }
    }
}
