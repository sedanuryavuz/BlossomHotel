using BlossomHotel.WebUI.Dtos.ContactDto;
using FluentValidation;

namespace BlossomHotel.WebUI.ValidationRules.ContactValidationRules
{
    public class CreateContactValidator:AbstractValidator<CreateContactDto>
    {
        public CreateContactValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim boş olamaz.")
                .MaximumLength(50).WithMessage("İsim en fazla 50 karakter olabilir.");
            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("E-posta boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi girin.");            

            RuleFor(x => x.MessageCategory)
                .NotEmpty().WithMessage("Mesaj kategorisi boş olamaz.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Mesaj boş olamaz.")
                .MaximumLength(500).WithMessage("Mesaj en fazla 500 karakter olabilir.");
        }
    }
}
