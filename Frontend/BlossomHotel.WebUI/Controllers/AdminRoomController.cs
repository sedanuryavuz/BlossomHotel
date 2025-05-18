using BlossomHotel.BusinessLayer.Abstract;
using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.WebUI.Dtos.HotelDto;
using BlossomHotel.WebUI.Dtos.RoomDto;
using BlossomHotel.WebUI.Models.Room;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace BlossomHotel.WebUI.Controllers
{
    public class AdminRoomController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminRoomController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7071/api/Room");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultRoomDto>>(result);
                return View(values);
            }
            else
            {
                ModelState.AddModelError("", "API kaynaklı bir hata oluştu.");
            }
            return View();
        }
        [HttpGet]
        //public IActionResult AddRoom()
        //{

        //    return View();
        //}
        public async Task<IActionResult> AddRoom()
        {
            var model = new CreateRoomViewModel();

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7071/api/Hotel");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var hotels = JsonConvert.DeserializeObject<List<ResultHotelDto>>(jsonData);

                model.Hotels = hotels;
            }
            else
            {
                model.Hotels = new List<ResultHotelDto>();
            }

            model.CreateRoomDto = new CreateRoomDto();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddRoom(CreateRoomDto createRoomDto)
        {
            if (!ModelState.IsValid)
            {
                var model = new CreateRoomViewModel();
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://localhost:7071/api/Hotel");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var hotels = JsonConvert.DeserializeObject<List<ResultHotelDto>>(jsonData);

                    model.Hotels = hotels;
                }
                else
                {
                    model.Hotels = new List<ResultHotelDto>();
                }

                model.CreateRoomDto = createRoomDto ?? new CreateRoomDto();

                return View(model);
            }

            var client2 = _httpClientFactory.CreateClient();

            var jsonDataPost = JsonConvert.SerializeObject(createRoomDto);
            var content = new StringContent(jsonDataPost, Encoding.UTF8, "application/json");

            var responsePost = await client2.PostAsync("https://localhost:7071/api/Room", content);

            if (responsePost.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Oda ekleme sırasında hata oluştu.");
                var model = new CreateRoomViewModel();

                var response = await client2.GetAsync("https://localhost:7071/api/Hotel");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var hotels = JsonConvert.DeserializeObject<List<ResultHotelDto>>(jsonData);
                    model.Hotels = hotels;
                }
                else
                {
                    model.Hotels = new List<ResultHotelDto>();
                }

                model.CreateRoomDto = createRoomDto;
                return View(model);
            }
        }

        public async Task<IActionResult> DeleteRoom(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7071/api/Room/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "API kaynaklı bir hata oluştu.");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateRoom(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseRoom = await client.GetAsync($"https://localhost:7071/api/Room/{id}");
            var responseHotels = await client.GetAsync("https://localhost:7071/api/Hotel");

            var model = new UpdateRoomViewModel();

            if (responseRoom.IsSuccessStatusCode)
            {
                var result = await responseRoom.Content.ReadAsStringAsync();
                model.UpdateRoomDto = JsonConvert.DeserializeObject<UpdateRoomDto>(result);
            }
            else
            {
                ModelState.AddModelError("", "Oda bilgisi alınamadı.");
            }

            if (responseHotels.IsSuccessStatusCode)
            {
                var jsonHotels = await responseHotels.Content.ReadAsStringAsync();
                model.Hotels = JsonConvert.DeserializeObject<List<ResultHotelDto>>(jsonHotels);
            }
            else
            {
                model.Hotels = new List<ResultHotelDto>();
            }

            return View(model);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRoom(UpdateRoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Aynı şekilde Hotels listesini doldurman gerekebilir.
                var client = _httpClientFactory.CreateClient();
                var responseHotels = await client.GetAsync("https://localhost:7071/api/Hotel");
                if (responseHotels.IsSuccessStatusCode)
                {
                    var jsonHotels = await responseHotels.Content.ReadAsStringAsync();
                    model.Hotels = JsonConvert.DeserializeObject<List<ResultHotelDto>>(jsonHotels);
                }
                else
                {
                    model.Hotels = new List<ResultHotelDto>();
                }
                return View(model);
            }

            var client2 = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(model.UpdateRoomDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client2.PutAsync("https://localhost:7071/api/Room", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "API kaynaklı bir hata oluştu.");
                // Hotels listesini tekrar doldur.
                var responseHotels = await client2.GetAsync("https://localhost:7071/api/Hotel");
                if (responseHotels.IsSuccessStatusCode)
                {
                    var jsonHotels = await responseHotels.Content.ReadAsStringAsync();
                    model.Hotels = JsonConvert.DeserializeObject<List<ResultHotelDto>>(jsonHotels);
                }
                else
                {
                    model.Hotels = new List<ResultHotelDto>();
                }
                return View(model);
            }
        }
    }
}
