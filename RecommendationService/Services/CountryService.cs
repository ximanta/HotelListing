using Neo4jClient;
using RecommendationService.Entities;

namespace RecommendationService.Services
{
    public class CountryService : ICountryService
    {
        private readonly IGraphClient _client;

        public CountryService(IGraphClient client)
        {
            _client = client;
        }

        public Country AddNewContry(Country country)
        {
            _client.Cypher.Create("(c:Country $country)")
                 .WithParam("country", country)
                 .ExecuteWithoutResultsAsync();
            return country;
        }

        public async Task<int> DeleteCountry(int id)
        {
            await _client.Cypher.Match("(c:Country)")
                .Where((Country c) => c.Id == id)
                .Delete("c")
                .ExecuteWithoutResultsAsync();

            return id;
        }

        public async Task<IEnumerable<Country>> GetallContries()
        {
            var countries = await _client.Cypher.Match("(c:Country)")
                .Return(c => c.As<Country>()).ResultsAsync;
            return countries;
        }

        public async Task<IEnumerable<Country>> GetById(int id)
        {
            var country = await _client.Cypher.Match("(c:Country)")
                .Where((Country c) => c.Id == id)
                .Return(c => c.As<Country>()).ResultsAsync;
            return country;
        }

        public async Task<Country> UpdateCountry(int id, Country country)
        {
            await _client.Cypher.Match("(c:Country)")
                .Where((Country c) => c.Id == id)
                .Set("c = $country")
                .WithParam("country", country)
                .ExecuteWithoutResultsAsync();
            return country;
        }
    }
}
