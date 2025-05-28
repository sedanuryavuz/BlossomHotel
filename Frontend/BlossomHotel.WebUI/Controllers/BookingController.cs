using BlossomHotel.BusinessLayer.Abstract;
using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.WebUI.Models.Booking;
using BlossomHotel.WebUI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlossomHotel.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Calisan,Musteri")]
    public class BookingController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailSender _emailSender;

        public BookingController(IRoomService roomService, IBookingService bookingService, UserManager<AppUser> userManager, EmailSender emailSender)
        {
            _roomService = roomService;
            _bookingService = bookingService;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        [HttpGet]
        public async Task<IActionResult> CreateBooking(int roomId)
        {
            var room = _roomService.TGetById(roomId);
            if (room == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var model = new CreateBookingViewModel
            {
                RoomId = room.RoomId,
                RoomName = room.RoomTitle,
                AppUserId = user!.Id,
                Name = user.Name,
                Mail = user.Email,
                Checkin = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(1),
                AdultCount = 1,
                ChildCount = 0,
                RoomCount = 1,
                RoomPrice = room.Price,
            };

            return View(model);
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateBooking(CreateBookingViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var user = await _userManager.GetUserAsync(User);
        //    var room = _roomService.TGetById(model.RoomId);
        //    if (user == null || room == null)
        //    {
        //        return NotFound();
        //    }

        //    var booking = new Booking
        //    {
        //        AppUserId = user.Id,
        //        RoomId = model.RoomId,
        //        Name = model.Name,
        //        Mail = model.Mail,
        //        Checkin = model.Checkin,
        //        CheckOut = model.CheckOut,
        //        AdultCount = model.AdultCount,
        //        ChildCount = model.ChildCount,
        //        RoomCount = model.RoomCount,
        //        SpecialRequest = model.SpecialRequest,
        //        Description = model.Description,
        //        Status = "Pending",
        //        CreatedAt = DateTime.Now
        //    };

        //    _bookingService.TInsert(booking);

        //    return RedirectToAction("Success");
        //}
        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            var room = _roomService.TGetById(model.RoomId);
            if (user == null || room == null)
            {
                return NotFound();
            }

            var existingBookings = _bookingService.TGetBookingsByRoomId(model.RoomId);

            bool isOverlap = existingBookings.Any(b =>
                !(model.CheckOut <= b.Checkin || model.Checkin >= b.CheckOut)
            );

            if (isOverlap)
            {
                ModelState.AddModelError(string.Empty, "Seçilen tarih aralığında bu oda için zaten başka bir rezervasyon bulunmaktadır.");
                return View(model);
            }

            int totalNights = (model.CheckOut - model.Checkin).Days;
            if (totalNights <= 0)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz tarih aralığı.");
                return View(model);
            }
            decimal adultRate = room.Price;
            decimal childRate = room.Price * 0.5m;

            decimal totalPrice = (model.AdultCount * adultRate + model.ChildCount * childRate) * totalNights;

            var booking = new Booking
            {
                AppUserId = user.Id,
                RoomId = model.RoomId,
                Name = model.Name,
                Mail = model.Mail,
                Checkin = model.Checkin,
                CheckOut = model.CheckOut,
                AdultCount = model.AdultCount,
                ChildCount = model.ChildCount,
                RoomCount = model.RoomCount,
                SpecialRequest = model.SpecialRequest,
                Description = model.Description,
                Status = "Pending",
                CreatedAt = DateTime.Now,
                TotalPrice = totalPrice,
            };

            _bookingService.TInsert(booking);

            var subject = "Rezervasyonunuz Alındı - Blossom Hotel";
            var body = $@"
        <h3>Merhaba {booking.Name},</h3>
        <p>Rezervasyonunuz başarıyla alınmıştır.</p>
        <ul>
            <li>Oda: {room.RoomTitle}</li>
            <li>Giriş: {booking.Checkin:dd.MM.yyyy}</li>
            <li>Çıkış: {booking.CheckOut:dd.MM.yyyy}</li>
            <li>Yetişkin: {booking.AdultCount}</li>
            <li>Çocuk: {booking.ChildCount}</li>
            <li>Toplam Tutar: ₺{booking.TotalPrice:F2}</li>
            <li>Durum: {booking.Status}</li>
        </ul>
        <p>Rezervasyonunuz onaylandığında tekrar bilgilendirileceksiniz.</p>
        <p>Teşekkür ederiz, Blossom Hotel</p>";

            await _emailSender.SendEmailAsync(booking.Name, booking.Mail, subject, body);

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}