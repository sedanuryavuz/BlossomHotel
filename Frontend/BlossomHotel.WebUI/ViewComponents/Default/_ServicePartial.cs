using BlossomHotel.WebUI.Dtos.ServiceDto;
using BlossomHotel.WebUI.Dtos.TestimonialDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlossomHotel.WebUI.ViewComponents.Default
{
    public class _ServicePartial: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ServicePartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task <IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7071/api/Service");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var serviceList = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData);
                return View(serviceList);
            }
            return View();
        }
    }
}
