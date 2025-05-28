using BlossomHotel.BusinessLayer.Abstract;
using BlossomHotel.WebUI.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlossomHotel.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminDashboardController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;

        public AdminDashboardController(IBookingService bookingService, IRoomService roomService)
        {
            _bookingService = bookingService;
            _roomService = roomService;
        }

        public IActionResult Index()
        {
            ViewBag.TotalRooms = _roomService.TGetTotalRoomCount();
            ViewBag.OccupiedRooms = _roomService.TGetOccupiedRoomCount();
            ViewBag.TotalRevenue = _bookingService.TGetTotalRevenue();

            ViewBag.RecentBookings = _bookingService.TGetRecentBookings(5);  
            ViewBag.UpcomingCheckOuts = _bookingService.TGetUpcomingCheckOuts(5);

            return View();
        }
    }
}
