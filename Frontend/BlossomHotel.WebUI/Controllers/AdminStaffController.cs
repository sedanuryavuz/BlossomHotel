using BlossomHotel.WebUI.Dtos.HotelDto;
using BlossomHotel.WebUI.Dtos.RoomDto;
using BlossomHotel.WebUI.Models.Room;
using BlossomHotel.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace BlossomHotel.WebUI.Controllers
{
    public class AdminStaffController : Controller
    {
        IHttpClientFactory _httpClientFactory;

        public AdminStaffController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7071/api/Staff");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<StaffViewModel>>(result);
                return View(values);
            }
            else
            {
                ModelState.AddModelError("", "API kaynaklı bir hata oluştu.");
            }
            return View(new List<StaffViewModel>());

        }
        [HttpGet]
        public async Task<IActionResult> AddStaff()
        {
            var model = new AddStaffViewModel();

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7071/api/Hotel"); // Burası api/Hotels olabilir, doğru endpoint'e dikkat et.

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var hotels = JsonConvert.DeserializeObject<List<ResultHotelDto>>(jsonData);

                // Otel listesini SelectListItem listesine dönüştür
                model.Hotels = hotels!.Select(h => new SelectListItem
                {
                    Text = h.HotelName,
                    Value = h.HotelId.ToString()
                }).ToList();
            }
            else
            {
                model.Hotels = new List<SelectListItem>();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff(AddStaffViewModel addStaffViewModel)
        {
            if (!ModelState.IsValid)
            {
                // Otel listesini tekrar doldur
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://localhost:7071/api/Hotel");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var hotels = JsonConvert.DeserializeObject<List<ResultHotelDto>>(result);

                    addStaffViewModel.Hotels = hotels!.Select(h => new SelectListItem
                    {
                        Text = h.HotelName,
                        Value = h.HotelId.ToString()
                    }).ToList();
                }
                else
                {
                    addStaffViewModel.Hotels = new List<SelectListItem>();
                }

                return View(addStaffViewModel);
            }

            // Burada API'nin beklediği Staff objesi oluşturuluyor
            var staffDto = new
            {
                Name = addStaffViewModel.Name,
                Surname = addStaffViewModel.Surname,
                WorkDepartment = addStaffViewModel.WorkDepartment,
                Gender = addStaffViewModel.Gender,
                ImageUrl = addStaffViewModel.ImageUrl,
                HotelId = addStaffViewModel.HotelId,
            };

            var clientPost = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(staffDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var postResponse = await clientPost.PostAsync("https://localhost:7071/api/Staff", stringContent);

            if (postResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Başarısızsa modeli tekrar göster
            return View(addStaffViewModel);
        }
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7071/api/Staff/{id}");
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
        public async Task<IActionResult> UpdateStaff(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7071/api/Staff/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var staff = JsonConvert.DeserializeObject<UpdateStaffViewModel>(jsonData);

            var responseHotels = await client.GetAsync("https://localhost:7071/api/Hotel");
            if (responseHotels.IsSuccessStatusCode)
            {
                var jsonHotels = await responseHotels.Content.ReadAsStringAsync();
                staff.Hotels = JsonConvert.DeserializeObject<List<ResultHotelDto>>(jsonHotels);
            }
            else
            {
                staff.Hotels = new List<ResultHotelDto>();
            }

            return View(staff);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStaff(UpdateStaffViewModel model)
        {
            if (!ModelState.IsValid)
            {
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

            // Burada sadece gerekli alanları DTO gibi seçiyoruz
            var dto = new
            {
                Id = model.StaffId,
                Name = model.Name,
                Surname = model.Surname,
                WorkDepartment = model.WorkDepartment,
                Gender = model.Gender,
                ImageUrl = model.ImageUrl,
                HotelId = model.HotelId,
                // Diğer API'nin beklediği alanlar burada
            };

            var jsonData = JsonConvert.SerializeObject(dto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client2.PutAsync("https://localhost:7071/api/Staff", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"API kaynaklı bir hata oluştu. Detay: {errorContent}");

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