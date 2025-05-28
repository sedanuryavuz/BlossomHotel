using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomHotel.EntityLayer.Enums
{
    public enum MessageCategory
    {
        Teşekkür= 1,  
        Şikayet = 2,     
        Öneri = 3,        
        Talep = 4,
        [Display(Name = "İş Görüşmesi")]
        IsGorusmesi = 5,
        [Display(Name = "Diğer (Belirtiniz)")]
        Diger = 6
    }
}
