using BlossomHotel.WebUI.Models.Staff;
using FluentValidation;

namespace BlossomHotel.WebUI.ValidationRules.StaffValidationRules
{
    public class CreateStaffValidator:AbstractValidator<AddStaffViewModel>
    {
        public CreateStaffValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("İsim boş olamaz.")
            .MaximumLength(50).WithMessage("İsim en fazla 50 karakter olabilir.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyisim boş olamaz.")
                .MaximumLength(50).WithMessage("Soyisim en fazla 50 karakter olabilir.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Cinsiyet seçilmelidir.")
                .Must(x => x == "Erkek" || x == "Kadın" || x == "Diğer")
                .WithMessage("Geçerli bir cinsiyet seçin.");

            RuleFor(x => x.WorkDepartment)
                .NotEmpty().WithMessage("Departman boş olamaz.")
                .MaximumLength(100).WithMessage("Departman en fazla 100 karakter olabilir.");

            RuleFor(x => x.HotelId)
                .GreaterThan(0).WithMessage("Lütfen bir otel seçin.");
        }
    }
}
