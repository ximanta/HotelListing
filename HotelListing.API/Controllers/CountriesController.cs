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

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
     
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public CountriesController(IMapper mapper, ICountryRepository countryRepository)
        {
            this._mapper = mapper;
            this._countryRepository = countryRepository;
        }

        // GET: api/Countries
        [HttpGet]
        /* Task class represents a single operation that does not 
         * return a value and that usually executes asynchronously.*/
        public async Task<ActionResult<IEnumerable<OutgoingCountryDTO>>> GetCountries()
        {
            var countries = await _countryRepository.GetAllAsync();
            var countryDTOs = _mapper.Map<List<OutgoingCountryDTO>>(countries);
            
            return Ok(countryDTOs) ;
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _countryRepository.GetDetails(id);

            if (country == null)
            {
                return NotFound();
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
                return NotFound("Country Not Found");
            }
           

            try
            {
                await _countryRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
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
                return NotFound();
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
