using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.data;
using HotelListing.API.Models.Country;
using AutoMapper;
using HotelListing.API.Repository;
using HotelListing.API.Exceptions;
using HotelListing.API.Models;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
     
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger<CountriesController> _logger;

        public CountriesController(IMapper mapper, ICountryRepository countryRepository, ILogger<CountriesController> logger)
        {
            this._mapper = mapper;
            this._countryRepository = countryRepository;
            this._logger = logger;
        }

        // GET: api/Countries
        // GET: api/Countries/GetAll
        [HttpGet("GetAll")]
        /* Task class represents a single operation that does not 
         * return a value and that usually executes asynchronously.*/
        public async Task<ActionResult<IEnumerable<OutgoingCountryDTO>>> GetCountries()
        {
            _logger.LogInformation("Request received for getting all countries at {DT}",
            DateTime.UtcNow.ToLongTimeString());
            var countries = await _countryRepository.GetAllAsync();
            var countryDTOs = _mapper.Map<List<OutgoingCountryDTO>>(countries);
            
            return Ok(countryDTOs) ;
        }
        /* GET: api/Countries/?StartIndex=0&pagesize=25&PageNumber=1*/
        [HttpGet]
        public async Task<ActionResult<PagedResult<OutgoingCountryDTO>>> GetPagedCountries([FromQuery] PageQueryParameters queryParameters)
        {
            var pagedCountriesResult = await _countryRepository.GetAllAsync<OutgoingCountryDTO>(queryParameters);
            return Ok(pagedCountriesResult);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _countryRepository.GetDetails(id);

            if (country == null)
            {
                throw new NotFoundException(nameof(GetCountry), id);
            }

            var countryDto = _mapper.Map<OutgoingCountryDTO>(country);

            return Ok(countryDto);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest("Invalid country ID");
            }
            var returnedCountry = await _countryRepository.GetAsync(id);

            if (returnedCountry == null)
            {
                throw new NotFoundException(nameof(PutCountry), id);
            }
           

            try
            {
                await _countryRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException dce)
            {
                if (!await CountryExists(id))
                {
                    throw new NotFoundException(nameof(PutCountry), id);
                }
                else
                {
                    _logger.LogError(dce, "Exception thrown at {DT}", DateTime.UtcNow.ToLongTimeString());

                     throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(IncomingCountryDTO countryDTO)
        {
            var country = _mapper.Map<Country>(countryDTO);
            await _countryRepository.AddAsync(country);        
            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countryRepository.GetAsync(id);
            if (country == null)
            {
                throw new NotFoundException(nameof(DeleteCountry), id);
            }

            await _countryRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countryRepository.Exists(id);
        }
    }
}
