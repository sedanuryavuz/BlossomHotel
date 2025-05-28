using BlossomHotel.WebUI.Dtos.AboutDto;
using BlossomHotel.WebUI.Dtos.HotelDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlossomHotel.WebUI.ViewComponents.Default
{
    public class _MainBannerPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
