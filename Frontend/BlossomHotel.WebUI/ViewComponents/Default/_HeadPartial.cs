using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlossomHotel.WebUI.ViewComponents.Default
{
    [AllowAnonymous]
    public class _HeadPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
