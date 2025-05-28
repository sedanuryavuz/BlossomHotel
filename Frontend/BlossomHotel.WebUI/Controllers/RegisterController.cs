using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.WebUI.Dtos.RegisterDto;
using Microsoft.AspNetCore.Authorization;

namespace BlossomHotel.WebUI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateNewUserDto createNewUserDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var appUser = new AppUser()
            {
                UserName = createNewUserDto.Mail,
                Email = createNewUserDto.Mail,
                Name = createNewUserDto.Name,
                Surname = createNewUserDto.Surname,
                PhoneNumber = createNewUserDto.PhoneNumber,
                ImageUrl = "/user/images/user-images/profil-fotograflari/default_pp.jpg",
            };
            var result = await _userManager.CreateAsync(appUser, createNewUserDto.Password!);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, "Musteri");
                return RedirectToAction("Index", "Login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
    }
}
