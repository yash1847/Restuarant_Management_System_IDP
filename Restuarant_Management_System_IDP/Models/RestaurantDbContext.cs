using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Models.ViewModels;

namespace Restuarant_Management_System_IDP.Models
{
    public class RestaurantDbContext : IdentityDbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Addresstb> Addressess { get; set; }

        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options){ }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                UserName = "admin",
                FullName = "Admin",
                Email = "admin@gmail.com",
                Contact = "9999999999",
                Password = "Admin123",
                Role = "Admin"
            });

        }

        
    }
}