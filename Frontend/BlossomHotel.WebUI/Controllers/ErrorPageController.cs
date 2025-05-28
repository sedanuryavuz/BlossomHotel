using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlossomHotel.WebUI.Controllers
{
    [AllowAnonymous]
    public class ErrorPageController : Controller
    {
        public IActionResult Error404()
        {
            Response.StatusCode = 404;
            ViewData["ErrorMessage"] = "Aradığınız sayfa bulunamadı.";
            return View();
        }
        public IActionResult AccessDenied()
        {
            Response.StatusCode = 403;
            ViewData["ErrorMessage"] = "Bu sayfaya erişim yetkiniz yok.";
            return View();
        }
        public IActionResult Unauthorized()
        {
            Response.StatusCode = 401;
            ViewData["ErrorMessage"] = "Bu işlem için giriş yapmanız gerekiyor.";
            return View();
        }
    }
}
