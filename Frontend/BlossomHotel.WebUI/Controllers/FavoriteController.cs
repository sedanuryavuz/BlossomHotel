using BlossomHotel.BusinessLayer.Abstract;
using BlossomHotel.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlossomHotel.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Calisan,Musteri")]

    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
            
        }
        public IActionResult ListFavorites(int userId)
        {
            var favorites = _favoriteService.TGetFavoritesByUserId(userId);
            return View(favorites);
        }
        [HttpPost]
        public IActionResult AddFavorite(int roomId, int userId)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return Unauthorized();
            }

            var currentUserId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            if (currentUserId != userId)
            {
                return Forbid(); // başka bir kullanıcı için işlem yapmaya çalışıyorsa engeller
            }

            var favorite = new Favorite
            {
                RoomId = roomId,
                AppUserId = currentUserId,
                CreatedAt = DateTime.Now
            };
            _favoriteService.TInsert(favorite);

            return RedirectToAction("RoomList", "Room");
        }


        public IActionResult DeleteFavorite(int favoriteId, int userId)
        {
            var favorite = _favoriteService.TGetById(favoriteId);
            if (favorite != null)
            {
                _favoriteService.TDelete(favorite);
            }

            return RedirectToAction("ListFavorites", new { userId = userId });
        }
    }
}
