using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Models.ViewModels;

namespace Restuarant_Management_System_IDP.Data
{
    public class RestaurantDbContext : IdentityDbContext<ApplicationUser>
    {
        //public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //public virtual DbSet<Usertb> Userstb { get; set; }
        public virtual DbSet<Addresstb> Addressess { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCart {  get; set; }
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ShoppingCart>().HasKey(x => new { x.Id, x.MenuItemId });

            modelBuilder.Entity<ShoppingCart>().HasKey(x => new { x.ApplicationUserId, x.MenuItemId });

            modelBuilder.Entity<Usertb>().HasData(new Usertb
            {
                UserId = 1,
                UserName = "admin",
                FullName = "Yaswanth",
                Email = "admin@gmail.com",
                Contact = "9090909090",
                Password = "Admin123",
                Role = "Admin"
            });

        }
        public DbSet<Restuarant_Management_System_IDP.Models.ViewModels.ProfileViewModel> ProfileViewModel { get; set; } = default!;


    }
}