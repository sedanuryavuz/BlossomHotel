using BlossomHotel.WebUI.Dtos.HotelDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlossomHotel.WebUI.ViewComponents.Default
{
    public class _OurHotelsPartial: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _OurHotelsPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7071/api/Hotel/withGallery");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultHotelWithGalleryDto>());

            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultHotelWithGalleryDto>>(jsonData);

            return View(values);
        }
    }
}
