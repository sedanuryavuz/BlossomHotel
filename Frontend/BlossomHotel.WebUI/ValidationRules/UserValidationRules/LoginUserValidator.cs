using BlossomHotel.WebUI.Dtos.LoginDto;
using FluentValidation;

namespace BlossomHotel.WebUI.ValidationRules.UserValidationRules
{
    public class LoginUserValidator:AbstractValidator<LoginUserDto>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("Mail adresinizi giriniz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifrenizi giriniz.");
        }
    }
}
