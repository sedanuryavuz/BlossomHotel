using BlossomHotel.WebUI.Dtos.AboutDto;
using BlossomHotel.WebUI.Dtos.HotelDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlossomHotel.WebUI.ViewComponents.AboutUs
{
    public class _HotelsPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _HotelsPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7071/api/Hotel/withGallery");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var aboutList = JsonConvert.DeserializeObject<List<ResultHotelWithGalleryDto>>(jsonData);
                return View(aboutList);
            }
            return View();
        }
    }
}
