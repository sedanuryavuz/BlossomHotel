using Microsoft.AspNetCore.Mvc;

namespace BlossomHotel.WebUI.ViewComponents.Default
{
    public class _AccountSidebarPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
