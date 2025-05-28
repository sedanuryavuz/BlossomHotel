using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.WebUI.Dtos.HotelDto;
using BlossomHotel.WebUI.Dtos.RoomDto;
using BlossomHotel.WebUI.Models.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Calisan")]
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
                var jsonData = await response.Content.ReadAsStringAsync();
                var rooms = JsonConvert.DeserializeObject<List<ResultRoomDto>>(jsonData);
                return View(rooms);
            }
            ModelState.AddModelError("", "API kaynaklı hata oluştu.");
            return View(new List<ResultRoomDto>());
        }

        [HttpGet]
        public async Task<IActionResult> AddRoom()
        {
            var model = new CreateRoomViewModel();

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7071/api/Hotel");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                model.Hotels = JsonConvert.DeserializeObject<List<ResultHotelDto>>(jsonData);
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
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://localhost:7071/api/Hotel");
                var model = new CreateRoomViewModel { CreateRoomDto = createRoomDto };
                if (response.IsSuccessStatusCode)
                {
                    var hotelsJsonInvalid = await response.Content.ReadAsStringAsync();
                    model.Hotels = JsonConvert.DeserializeObject<List<ResultHotelDto>>(hotelsJsonInvalid);
                }
                else
                {
                    model.Hotels = new List<ResultHotelDto>();
                }
                return View(model);
            }

            var clientHttp = _httpClientFactory.CreateClient();
            var content = new MultipartFormDataContent();

           
            content.Add(new StringContent(createRoomDto.RoomNumber ?? ""), "RoomNumber");
            content.Add(new StringContent(createRoomDto.RoomTitle ?? ""), "RoomTitle");
            content.Add(new StringContent(createRoomDto.Price.ToString()), "Price");
            content.Add(new StringContent(createRoomDto.BedCount ?? ""), "BedCount");
            content.Add(new StringContent(createRoomDto.BathCount ?? ""), "BathCount");
            content.Add(new StringContent(createRoomDto.Description ?? ""), "Description");
            content.Add(new StringContent(createRoomDto.HotelId.ToString()), "HotelId");

            if (createRoomDto.RoomCoverImage != null)
            {
                var coverStream = createRoomDto.RoomCoverImage.OpenReadStream();
                content.Add(new StreamContent(coverStream), "CoverImage", createRoomDto.RoomCoverImage.FileName);
            }

            if (createRoomDto.RoomGallery != null && createRoomDto.RoomGallery.Any())
            {
                foreach (var image in createRoomDto.RoomGallery)
                {
                    var imageStream = image.OpenReadStream();
                    content.Add(new StreamContent(imageStream), "RoomImages", image.FileName);
                }
            }

            var responsePost = await clientHttp.PostAsync("https://localhost:7071/api/Room", content);

            if (responsePost.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Oda eklenirken hata oluştu.");
                var model = new CreateRoomViewModel { CreateRoomDto = createRoomDto };

                var responseHotels = await clientHttp.GetAsync("https://localhost:7071/api/Hotel");
                if (responseHotels.IsSuccessStatusCode)
                {
                    var hotelsJsonError = await responseHotels.Content.ReadAsStringAsync();
                    model.Hotels = JsonConvert.DeserializeObject<List<ResultHotelDto>>(hotelsJsonError);
                }
                else
                {
                    model.Hotels = new List<ResultHotelDto>();
                }

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
            ModelState.AddModelError("", "API kaynaklı hata oluştu.");
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
                var jsonData = await responseRoom.Content.ReadAsStringAsync();
                model.UpdateRoomDto = JsonConvert.DeserializeObject<UpdateRoomDto>(jsonData);
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

        [HttpPost]
        public async Task<IActionResult> UpdateRoom(UpdateRoomViewModel model)
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
            var content = new MultipartFormDataContent();

            var dto = model.UpdateRoomDto;

            content.Add(new StringContent(dto!.RoomTitle ?? ""), nameof(dto.RoomTitle));
            content.Add(new StringContent(dto.Description ?? ""), nameof(dto.Description));
            content.Add(new StringContent(dto.HotelId.ToString() ?? ""), nameof(dto.HotelId));
            content.Add(new StringContent(dto.RoomNumber!.ToString()), nameof(dto.RoomNumber));
            content.Add(new StringContent(dto.BedCount!.ToString()), nameof(dto.BedCount));
            content.Add(new StringContent(dto.BathCount!.ToString()), nameof(dto.BathCount));
            content.Add(new StringContent(dto.Price.ToString()), nameof(dto.Price));
            content.Add(new StringContent(dto.RoomId.ToString()), nameof(dto.RoomId)); 

            if (dto.CoverImage != null)
            {
                var coverStream = dto.CoverImage.OpenReadStream();
                content.Add(new StreamContent(coverStream), nameof(dto.CoverImage), dto.CoverImage.FileName);
            }

            if (dto.NewRoomImages != null && dto.NewRoomImages.Any())
            {
                foreach (var image in dto.NewRoomImages)
                {
                    var imageStream = image.OpenReadStream();
                    content.Add(new StreamContent(imageStream), nameof(dto.NewRoomImages), image.FileName);
                }
            }

            var response = await client2.PutAsync("https://localhost:7071/api/Room", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"API kaynaklı hata oluştu: {errorContent}");

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


        [HttpGet("DeleteImg")]
        public async Task<IActionResult> DeleteImage(int id, int roomId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7071/api/RoomImage/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("UpdateRoom", new { id = roomId });
            }
            else
            {
                TempData["ImageDeleteError"] = "Fotoğraf silinemedi.";
                return RedirectToAction("UpdateRoom", new { id = roomId });
            }
        }


    }
}
