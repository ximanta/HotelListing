using AutoMapper;
using HotelListing.API.data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly HotelListDbContext _context;

        public CountryRepository(HotelListDbContext _context, IMapper mapper) : base(_context, mapper)
        {
           this._context = _context;
        }
        public async Task<Country> GetDetails(int id)
        {
            return await _context.Countries.Include(q => q.Hotels)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
