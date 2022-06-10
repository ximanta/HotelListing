using AutoMapper;
using UserService.Dtos;
using UserService.entities;

namespace HotelListing.API.config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<UserProfile, IncommingUserProfileDto>().ReverseMap();
            CreateMap<UserProfile, OutgoingUserProfileDto>().ReverseMap();
        }
    }
}
