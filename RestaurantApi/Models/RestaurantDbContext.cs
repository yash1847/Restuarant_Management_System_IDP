using Microsoft.EntityFrameworkCore;
using Restuarant_Management_System_IDP.Models;

namespace RestaurantApi.Models
{
    public class RestaurantDbContext : DbContext
    {
        //public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


    }
}