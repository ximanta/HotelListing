using AutoMapper;
using HotelListing.API.data;
using HotelListing.API.Models.Country;

namespace HotelListing.API.config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, IncomingCountryDTO>().ReverseMap();
            CreateMap<Country, OutgoingCountryDTO>().ReverseMap();
        }
    }
}
