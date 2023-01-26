using Microsoft.EntityFrameworkCore;
using Transflo_Task.Models;

namespace Transflo_Task.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }


        public DbSet<Driver> Drivers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Driver>().HasData(
                new Driver
                {
                    Id = 1,
                    FirstName = "Shaaban",
                    LastName = "Eita",
                    Email = "Shabaneita@gmail.com",
                    PhoneNumber = "+201116039373",

                },
              new Driver
              {
                  Id = 2,
                  FirstName = "Premium Pool Driver",
                  LastName = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  Email = "Shabaneita@gmail.com",
                  PhoneNumber = "+201116039373",
              },
              new Driver
              {
                  Id = 3,
                  FirstName = "Luxury Pool Driver",
                  LastName = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  Email = "Shabaneita@gmail.com",
                  PhoneNumber = "+201116039373",
              },
              new Driver
              {
                  Id = 4,
                  FirstName = "Diamond Driver",
                  LastName = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  Email = "Shabaneita@gmail.com",
                  PhoneNumber = "+201116039373",
              },
              new Driver
              {
                  Id = 5,
                  FirstName = "Diamond Pool Driver",
                  LastName = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  Email = "Shabaneita@gmail.com",
                  PhoneNumber = "+201116039373",
              });
        }
    }
}
