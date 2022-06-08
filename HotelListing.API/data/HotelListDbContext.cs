
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.data
{
    public class HotelListDbContext: DbContext
    {
        public HotelListDbContext(DbContextOptions options):base(options)
        { 

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
