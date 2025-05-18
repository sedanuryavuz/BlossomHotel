using AutoMapper;
using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.WebUI.Dtos.LoginDto;
using BlossomHotel.WebUI.Dtos.RegisterDto;

namespace BlossomHotel.WebUI.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CreateNewUserDto, AppUser>().ReverseMap();

            CreateMap<LoginUserDto, AppUser>().ReverseMap();
        }
    }
}
