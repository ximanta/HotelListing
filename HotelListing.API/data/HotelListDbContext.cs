
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name="India",
                    CountryCode="IND"
                },
                    new Country
                    {
                        Id = 2,
                        Name = "United Kingdoms",
                        CountryCode = "UK"
                    }
                );
            modelBuilder.Entity<Hotel>().HasData(
               new Hotel
               {
                   Id = 1,
                   Name = "Seagull Resorts",
                   CountryId = 2,
                   Address="Park Street",
                   Rating=4
               },
                    new Hotel
                    {
                        Id = 2,
                        Name = "Maryland",
                        CountryId = 1,
                        Address = "12th Peek Road",
                        Rating = 5
                    }
               ); 


        }
    }
}
