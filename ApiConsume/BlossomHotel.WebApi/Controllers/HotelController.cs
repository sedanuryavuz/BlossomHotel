using BlossomHotel.BusinessLayer.Abstract;
using BlossomHotel.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossomHotel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        [HttpGet]
        public IActionResult HotelList()
        {
            var values = _hotelService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddHotel(Hotel hotel)
        {
            _hotelService.TInsert(hotel);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateHotel(Hotel hotel)
        {
            _hotelService.TUpdate(hotel);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            var values = _hotelService.TGetById(id);
            _hotelService.TDelete(values);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetHotelById(int id)
        {
            var values = _hotelService.TGetById(id);
            return Ok(values);
        }
    }
}
