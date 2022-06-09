using HotelListing.API.data;

namespace HotelListing.API.Repository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<Country> GetDetails(int id);
    }
}
