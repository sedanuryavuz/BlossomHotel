using BlossomHotel.BusinessLayer.Abstract;
using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.WebUI.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlossomHotel.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Calisan,Musteri")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IFavoriteService _favoriteService;
        private readonly IBookingService _bookingService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IBookingService bookingService, IFavoriteService favoriteService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bookingService = bookingService;
            _favoriteService = favoriteService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var model = new UpdateUserViewModel
            {
                Name = user.Name,
                Surname = user.Surname,
                ExistingPhotoPath = user.ImageUrl
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            user.Name = model.Name;
            user.Surname = model.Surname;

            if (model.ProfilePhoto != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profile");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProfilePhoto.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfilePhoto.CopyToAsync(stream);
                }

                user.ImageUrl = "/images/profile/" + uniqueFileName;
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Index", model);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (User.IsInRole("Admin"))
            {
                return View("ChangePasswordAdmin");  
            }
            else
            {
                return View("ChangePasswordUser"); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (User.IsInRole("Admin"))
                    return View("ChangePasswordAdmin", model);
                else
                    return View("ChangePasswordUser", model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword!, model.NewPassword!);

            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["Success"] = "Şifre başarıyla güncellendi.";

                return RedirectToAction("ChangePassword", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            if (User.IsInRole("Admin"))
                return View("ChangePasswordAdmin", model);
            else
                return View("ChangePasswordUser", model);
        }
        [HttpPost]
        public IActionResult AddFavorite(int roomId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var existingFavorite = _favoriteService.TGetFavoritesByUserId(userId)
                                                   .FirstOrDefault(f => f.RoomId == roomId);

            if (existingFavorite != null)
            {
                TempData["message"] = "Bu oda zaten favorilerinizde var.";
                return RedirectToAction("MyFavorites");
            }

            var favorite = new Favorite
            {
                RoomId = roomId,
                AppUserId = userId
            };

            _favoriteService.TInsert(favorite);
            TempData["message"] = "Oda favorilere eklendi.";
            return RedirectToAction("MyFavorites");
        }

        public IActionResult MyFavorites()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var favorites = _favoriteService.TGetFavoritesByUserId(userId);
            return View(favorites);
        }
        [HttpGet]
        public IActionResult DeleteFavorite(int favoriteId)
        {
            var favorite = _favoriteService.TGetById(favoriteId);
            if (favorite != null)
            {
                _favoriteService.TDelete(favorite);
                TempData["message"] = "Favori başarıyla silindi.";
            }
            else
            {
                TempData["error"] = "Favori bulunamadı.";
            }

            return RedirectToAction("MyFavorites");
        }

        public IActionResult MyBookings()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var bookings = _bookingService.TGetBookingsByUserId(userId);
            return View(bookings);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Identity.Application");
            return RedirectToAction("Index", "Default");
        }


    }
}
