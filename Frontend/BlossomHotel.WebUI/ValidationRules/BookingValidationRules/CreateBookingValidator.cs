using BlossomHotel.WebUI.Models.Booking;
using FluentValidation;

namespace BlossomHotel.WebUI.ValidationRules.BookingValidationRules
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingViewModel>
    {
        public CreateBookingValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("İsim boş olamaz.")
            .MaximumLength(50).WithMessage("İsim en fazla 50 karakter olabilir.");

            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi girin.");

            RuleFor(x => x.Checkin)
                .NotEmpty().WithMessage("Giriş tarihi boş olamaz.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Giriş tarihi bugünden önce olamaz.");

            RuleFor(x => x.CheckOut)
                .NotEmpty().WithMessage("Çıkış tarihi boş olamaz.")
                .GreaterThan(x => x.Checkin).WithMessage("Çıkış tarihi giriş tarihinden sonra olmalıdır.");

            RuleFor(x => x.AdultCount)
                .GreaterThan(0).WithMessage("Yetişkin sayısı en az 1 olmalıdır.")
                .GreaterThanOrEqualTo(0).WithMessage("Yetişkin sayısı negatif olamaz.");

            RuleFor(x => x.ChildCount)
                .GreaterThanOrEqualTo(0).WithMessage("Çocuk sayısı negatif olamaz.");

            RuleFor(x => x.SpecialRequest)
                .MaximumLength(500).WithMessage("Özel istek en fazla 500 karakter olabilir.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Açıklama en fazla 1000 karakter olabilir.");
        }
    }
}
