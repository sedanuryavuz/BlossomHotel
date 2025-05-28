using AutoMapper;
using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.WebUI.Dtos.ContactDto;
using BlossomHotel.WebUI.Dtos.LoginDto;
using BlossomHotel.WebUI.Dtos.RegisterDto;
using BlossomHotel.WebUI.Dtos.RoomDto;

namespace BlossomHotel.WebUI.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CreateNewUserDto, AppUser>().ReverseMap();

            CreateMap<LoginUserDto, AppUser>().ReverseMap();

            CreateMap<CreateRoomDto, Room>().ReverseMap();

            CreateMap<CreateContactDto, Contact>().ReverseMap();
        }
    }
}
