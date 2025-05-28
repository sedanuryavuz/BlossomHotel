using BlossomHotel.WebUI.Dtos.AboutDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlossomHotel.WebUI.ViewComponents.Default
{
    public class _FooterPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _FooterPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7071/api/About");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var aboutList = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
                return View(aboutList);
            }
            return View();
        }
    }
}
