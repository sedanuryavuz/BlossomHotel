using BlossomHotel.WebUI.Models.Mail;
using BlossomHotel.WebUI.Services;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace BlossomHotel.WebUI.Controllers
{
    public class AdminMailController : Controller
    {
        private readonly EmailSender _emailSender;

        public AdminMailController(EmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }        
        [HttpPost]
        public async Task<IActionResult> Index(AdminMailViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _emailSender.SendEmailAsync(model.Name!, model.ReceiverMail!, model.Subject!, model.Body!);
                ViewBag.Message = "Mail başarıyla gönderildi.";
            }
            return View(model);
        }
    }
}
