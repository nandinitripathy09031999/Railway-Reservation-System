using Microsoft.EntityFrameworkCore;
using Railway_Reservation_System.Models;

namespace Railway_Reservation_System.Data
{
    public class RailwayRSDbContext:DbContext
    {
        public RailwayRSDbContext(DbContextOptions<RailwayRSDbContext> options):base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Train> Trains { get; set; }

    }
}
