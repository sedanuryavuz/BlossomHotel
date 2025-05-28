using BlossomHotel.WebUI.Dtos.HotelDto;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BlossomHotel.WebUI.ViewComponents.Default
{
    public class _SearchBoxPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _SearchBoxPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7071/api/Hotel");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<ResultHotelDto>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var hotels = JsonSerializer.Deserialize<List<ResultHotelDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(hotels);
        }
    }
}
