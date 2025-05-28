using BlossomHotel.WebUI.Dtos.RegisterDto;
using FluentValidation;

namespace BlossomHotel.WebUI.ValidationRules.UserValidationRules
{
    public class RegisterUserValidator : AbstractValidator<CreateNewUserDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad alanı gereklidir.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyad alanı gereklidir.");

            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("Mail alanı gereklidir.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası alanı gereklidir.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı gereklidir.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Şifre Tekrar alanı gereklidir.")
                .Equal(x => x.Password).WithMessage("Şifreler uyuşmuyor.");
        }
    }
}
