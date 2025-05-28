using BlossomHotel.WebUI.Dtos.HotelDto;
using FluentValidation;

namespace BlossomHotel.WebUI.ValidationRules.HotelValidationRules
{
    public class UpdateHotelValidator:AbstractValidator<UpdateHotelDto>
    {
        public UpdateHotelValidator()
        {
            RuleFor(x => x.HotelName)
           .NotEmpty().WithMessage("Otel adı boş olamaz.")
           .MaximumLength(100).WithMessage("Otel adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres boş olamaz.")
                .MaximumLength(200).WithMessage("Adres en fazla 200 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz.")
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
        }
    }
}
