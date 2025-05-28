using Microsoft.AspNetCore.Mvc;

namespace BlossomHotel.WebUI.ViewComponents.Default
{
    public class _NavbarPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
