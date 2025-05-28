using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.WebUI.Dtos.LoginDto;
using BlossomHotel.WebUI.Models.Mail;
using BlossomHotel.WebUI.Models.User;
using BlossomHotel.WebUI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlossomHotel.WebUI.Controllers
{
    [AllowAnonymous]

    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly EmailSender _emailSender;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, EmailSender emailSender, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _emailSender = emailSender;
            _userManager = userManager;
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Bu email adresine kayıtlı kullanıcı bulunamadı.");
                return View(model);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPassword", "Login", new { token, email = user.Email }, Request.Scheme);

            await _emailSender.SendEmailAsync(
                toName: user.UserName,
                toEmail: user.Email,
                subject: "Şifre Sıfırlama",
                body: $"Şifrenizi sıfırlamak için aşağıdaki linke tıklayınız:\n{resetLink}"
            );

            return View("ForgotPasswordConfirmation");
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ResetPasswordViewModel
            {
                Token = token,
                Email = email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        //[HttpGet]
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync("Identity.Application");

        //    if (User.IsInRole("Admin"))
        //    {
        //        return RedirectToAction("Index", "AdminLogin");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}
    }
}
