using AutoMapper;
using UserService.Dtos;
using UserService.entities;

namespace HotelListing.API.config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, IncommingUserDto>().ReverseMap();
            CreateMap<User, OutgoingUserDto>().ReverseMap();
        }
    }
}
