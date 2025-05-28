using BlossomHotel.BusinessLayer.Abstract;
using BlossomHotel.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossomHotel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpGet]
        public IActionResult ContactList()
        {
            var values = _contactService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddContact(Contact contact)
        {
            _contactService.TInsert(contact);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateContact(Contact contact)
        {
            _contactService.TUpdate(contact);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var values = _contactService.TGetById(id);
            _contactService.TDelete(values);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetContactById(int id)
        {
            var values = _contactService.TGetById(id);
            return Ok(values);
        }
    }
}
