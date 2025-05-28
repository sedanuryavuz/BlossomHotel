using BlossomHotel.BusinessLayer.Abstract;
using BlossomHotel.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossomHotel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }
        [HttpGet]
        public IActionResult TestimonialList()
        {
            var values = _testimonialService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult TestimonialAdd(Testimonial testimonial)
        {
            _testimonialService.TInsert(testimonial);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult TestimonialDelete(int id)
        {
            var testimonial = _testimonialService.TGetById(id);
            _testimonialService.TDelete(testimonial);
            return Ok();
        }
        [HttpPut]
        public IActionResult TestimonialUpdate(Testimonial testimonial)
        {
            _testimonialService.TUpdate(testimonial);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult TestimonialGetById(int id)
        {
            var testimonial = _testimonialService.TGetById(id);
            return Ok(testimonial);
        }
    }
}
