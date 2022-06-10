
using Microsoft.EntityFrameworkCore;
using UserService.entities;

namespace HotelListing.API.Data
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions options):base(options)
        { 

        }
  
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name="Andy Crawford",
                    EmailId = "andy@example.com",
                    Password="Andy",
                    Gender='M',
                    Age=24
                },
                    new User
                    {
                        Id = 2,
                        Name = "Julia Jackson",
                        EmailId = "julia@example.com",
                        Password = "Julia",
                        Gender = 'F',
                        Age = 22
                    },
                          new User
                          {
                              Id = 3,
                              Name = "Arun Lal",
                              EmailId = "arun@example.com",
                              Password = "Arun",
                              Gender = 'M',
                              Age = 25
                          }
                );
            


        }
    }
}
