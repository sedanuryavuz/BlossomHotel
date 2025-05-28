using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.WebUI.Dtos.LoginDto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlossomHotel.WebUI.Controllers
{
    [AllowAnonymous] 
    public class AdminLoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public AdminLoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginUserDto loginUserDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginUserDto.Mail!, loginUserDto.Password!, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "AdminDashboard");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            bool isAdmin = User.IsInRole("Admin");

            await HttpContext.SignOutAsync("Identity.Application");

            if (isAdmin)
            {
                return RedirectToAction("Index", "AdminLogin");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
