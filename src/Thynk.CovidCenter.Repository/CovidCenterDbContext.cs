using Microsoft.EntityFrameworkCore;
using Thynk.CovidCenter.Data.Models;

namespace Thynk.CovidCenter.Repository
{
    public class CovidCenterDbContext : DbContext
    {

        public CovidCenterDbContext(DbContextOptions<CovidCenterDbContext> options)
            : base(options)
        {
        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<AvailableDate> AvailableDates { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AvailableDate>()
               .HasIndex(r => new { r.DateAvailable });

            modelBuilder.Entity<ApplicationUser>()
               .HasIndex(t => new { t.Email })
               .IsUnique();
        }
    }
}
