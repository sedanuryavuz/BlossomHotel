using BlossomHotel.BusinessLayer.Abstract;
using BlossomHotel.WebUI.Dtos.AboutDto;
using BlossomHotel.WebUI.Dtos.BookingDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlossomHotel.WebUI.Controllers
{
    [AllowAnonymous]
    public class AboutController : Controller
    {
        IHttpClientFactory _httpClientFactory;
        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7071/api/About");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(result);
                return View(values);
            }
            else
            {
                ModelState.AddModelError("", "API kaynaklı bir hata oluştu.");
            }
            return View();
        }
    }
}
