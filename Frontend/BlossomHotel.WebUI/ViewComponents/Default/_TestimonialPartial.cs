using BlossomHotel.WebUI.Dtos.TestimonialDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlossomHotel.WebUI.ViewComponents.Default
{
    public class _TestimonialPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _TestimonialPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task <IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7071/api/Testimonial");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var testimonialList = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(jsonData);
                return View(testimonialList);
            }
            return View();
        }
    }
}
