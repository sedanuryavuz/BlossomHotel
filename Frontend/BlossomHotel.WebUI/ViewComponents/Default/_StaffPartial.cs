using BlossomHotel.WebUI.Dtos.AboutDto;
using BlossomHotel.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlossomHotel.WebUI.ViewComponents.Default
{
    public class _StaffPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _StaffPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7071/api/Staff");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var aboutList = JsonConvert.DeserializeObject<List<StaffViewModel>>(jsonData);
                return View(aboutList);
            }
            return View();
        }
    }
}
