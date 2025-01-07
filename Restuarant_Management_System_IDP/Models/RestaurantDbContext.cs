using Microsoft.EntityFrameworkCore;
using Restuarant_Management_System_IDP.Models;

namespace Restuarant_Management_System_IDP.Models
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options){ }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                    }

        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Addresstb> Addressess { get; set; }
    }
}