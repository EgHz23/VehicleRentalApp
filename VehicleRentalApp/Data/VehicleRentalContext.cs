using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Za IdentityDbContext
using Microsoft.EntityFrameworkCore; // Za DbContext
using VehicleRentalApp.Models;

namespace VehicleRentalApp.Data
{
    public class VehicleRentalContext : IdentityDbContext<ApplicationUser>
    {
        public VehicleRentalContext(DbContextOptions<VehicleRentalContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
