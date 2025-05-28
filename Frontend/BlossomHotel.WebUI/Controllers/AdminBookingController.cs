using BlossomHotel.WebUI.Dtos.BookingDto;
using BlossomHotel.WebUI.Services;
using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BlossomHotel.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Calisan")]
    public class AdminBookingController : Controller
    {
        IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AdminBookingController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7071/api/Booking");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBookingDto>>(result);
                return View(values);
            }
            else
            {
                ModelState.AddModelError("", "API kaynaklı bir hata oluştu.");
            }
            return View();
        }
        //public async Task<IActionResult> ApprovedReservation(int id)
        //{
        //    var client = _httpClientFactory.CreateClient();

        //    var responseMessage = await client.GetAsync($"https://localhost:7071/api/Booking/BookingApproved?id={id}");
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "API kaynaklı bir hata oluştu.");
        //        return View();
        //    }
        //}

        public async Task<IActionResult> CancelReservation(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync($"https://localhost:7071/api/Booking/BookingCancel?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "API kaynaklı bir hata oluştu.");
                return View();
            }
        }
        public async Task<IActionResult> WaitReservation(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync($"https://localhost:7071/api/Booking/BookingWait?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "API kaynaklı bir hata oluştu.");
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateBooking(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7071/api/Booking/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateBookingDto>(result);
                return View(values);
            }
            else
            {
                ModelState.AddModelError("", "API kaynaklı bir hata oluştu.");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateBookingDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://localhost:7071/api/Booking/UpdateBooking", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> ApprovedReservation(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var getResponse = await client.GetAsync($"https://localhost:7071/api/Booking/{id}");
            if (!getResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Rezervasyon alınamadı.");
                return View("Index");
            }

            var jsonResult = await getResponse.Content.ReadAsStringAsync();
            var booking = JsonConvert.DeserializeObject<ResultBookingDto>(jsonResult);

            var responseMessage = await client.GetAsync($"https://localhost:7071/api/Booking/BookingApproved?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var emailSender = new EmailSender(_configuration);
                await emailSender.SendEmailAsync(
                    booking.Name,
                    booking.Mail,
                    "Rezervasyon Onaylandı",
                    $"Sayın {booking.Name}, {booking.Checkin:dd.MM.yyyy} - {booking.CheckOut:dd.MM.yyyy} tarihleri arasındaki rezervasyonunuz onaylandı. Blossom Hotel ailesi olarak iyi tatiller dileriz."
                );

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Rezervasyon onaylanırken hata oluştu.");
                return View("Index");
            }
        }

    }
}