using RecommendationService.Entities;

namespace RecommendationService.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetallContries();
        Task<IEnumerable<Country>> GetById(int id);
        Country AddNewContry(Country country);
        Task<Country> UpdateCountry(int id, Country country);
        Task<int> DeleteCountry(int id);

    }
}
