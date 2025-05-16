using AutoMapper;
using BlossomHotel.DtoLayer.Dtos.RoomDto;
using BlossomHotel.EntityLayer.Concrete;

namespace BlossomHotel.WebApi.Mapping
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AddRoomDto, Room>().ReverseMap();

            CreateMap<UpdateRoomDto, Room>().ReverseMap();
        }
    }
}
