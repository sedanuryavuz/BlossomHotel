using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.DtoLayer.Dtos.RoomDto
{
    public class UpdateRoomDto
    {
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Lütfen oda numarasını yazınız.")]
        public string? RoomNumber { get; set; }
        [Required(ErrorMessage = "Lütfen oda başlığı bilgisi giriniz.")]

        public string? RoomTitle { get; set; }
        public string? RoomCoverImage { get; set; }
        [Required(ErrorMessage = "Lütfen yatak sayısı giriniz.")]
        public string? BedCount { get; set; }
        [Required(ErrorMessage = "Lütfen banyo sayısı giriniz.")]
        public string? BathCount { get; set; }
        [Required(ErrorMessage = "Lütfen açıklama bilgisi giriniz.")]
        [StringLength(1000, ErrorMessage = "Açıklama 1000 karakterden fazla olamaz.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Lütfen fiyat bilgisi giriniz.")]
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
