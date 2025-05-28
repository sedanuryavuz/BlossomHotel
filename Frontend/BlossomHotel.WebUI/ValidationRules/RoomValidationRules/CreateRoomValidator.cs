using BlossomHotel.WebUI.Dtos.RoomDto;
using FluentValidation;

namespace BlossomHotel.WebUI.ValidationRules.RoomValidationRules
{
    public class CreateRoomValidator:AbstractValidator<CreateRoomDto>
    {
        public CreateRoomValidator()
        {
            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("Lütfen bir otel seçin.");


            RuleFor(x => x.RoomNumber)
                .NotEmpty().WithMessage("Oda numarası boş olamaz.")
                .MaximumLength(10).WithMessage("Oda numarası en fazla 10 karakter olabilir.");

            RuleFor(x => x.RoomTitle)
                .NotEmpty().WithMessage("Oda başlığı boş olamaz.")
                .MaximumLength(100).WithMessage("Oda başlığı en fazla 100 karakter olabilir.");

            RuleFor(x => x.BedCount)
                .NotEmpty().WithMessage("Yatak sayısı girilmelidir.");

            RuleFor(x => x.BathCount)
                .NotEmpty().WithMessage("Banyo sayısı girilmelidir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama girilmelidir.")
                .MaximumLength(1000).WithMessage("Açıklama en fazla 1000 karakter olabilir.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.");

            RuleFor(x => x.HotelId)
                .GreaterThan(0).WithMessage("Geçerli bir otel seçilmelidir.");

            RuleFor(x => x.RoomCoverImage)
                .NotNull().WithMessage("Kapak fotoğrafı seçilmelidir.")
                .Must(BeAnImage).WithMessage("Kapak fotoğrafı sadece görsel formatında olmalıdır (jpg, png, jpeg).");

            RuleFor(x => x.RoomGallery)
                .NotNull().WithMessage("Oda fotoğrafları seçilmelidir.")
                .Must(x => x != null && x.Any()).WithMessage("En az bir fotoğraf seçilmelidir.");

            RuleForEach(x => x.RoomGallery)
                .Must(BeAnImage).WithMessage("Galeri fotoğrafları sadece görsel formatında olmalıdır (jpg, png, jpeg).");

        }
        private bool BeAnImage(IFormFile? file)
        {
            if (file == null)
                return true; 

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = System.IO.Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(extension);
        }
    }
}
