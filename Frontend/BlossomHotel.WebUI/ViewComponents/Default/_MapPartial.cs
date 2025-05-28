using Microsoft.AspNetCore.Mvc;

namespace BlossomHotel.WebUI.ViewComponents.Default
{
    public class _MapPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
