using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using RecommendationService.Entities;
using RecommendationService.Services;

namespace RecommendationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly IGraphClient _client;
        private readonly ICountryService _countryService;

        public CountryController(IGraphClient _client, ICountryService countryService)
        {
            this._client = _client;
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<ActionResult<Country>> Get() {
            var countries = await _countryService.GetallContries();
            return Ok(countries);
        }

        [HttpPost]
        public ActionResult<Country> Create([FromBody] Country country) {
            _countryService.AddNewContry(country);
            return CreatedAtAction(nameof(Create), country);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var country = await _countryService.GetById(id);
            return Ok(country);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Country country) {
             await _countryService.UpdateCountry(id, country);
            return Ok(country);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            _countryService.DeleteCountry(id);
            return Ok("Deleted successfully");
        }
    }
}
