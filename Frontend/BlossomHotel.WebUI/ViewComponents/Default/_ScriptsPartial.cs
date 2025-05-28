using Microsoft.AspNetCore.Mvc;

namespace BlossomHotel.WebUI.ViewComponents.Default
{
    public class _ScriptsPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
