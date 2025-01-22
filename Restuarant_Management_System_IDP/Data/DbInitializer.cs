using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restuarant_Management_System_IDP.Models;
using System.Diagnostics;

namespace Restuarant_Management_System_IDP.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly RestaurantDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(RestaurantDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) 
        { 
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("####", ex.Message);
            }

            if(!_db.Roles.Any(r => r.Name == SD.Admin))
            {
                //Create roles
                _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Kitchen)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.FrontDesk)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
            }
            //return;
            if(!_db.Users.Any(u => u.UserName == "admin"))
            {
                ApplicationUser adminuser = new ApplicationUser()
                {
                    UserName = "admin_user",
                    FullName = "Admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "9090909090"
                };
                string adminpassword = "Admin@123";
                //default admin
                //var result = _userManager.CreateAsync(adminuser);
                 var result = await _userManager.CreateAsync(adminuser, adminpassword);

                if (result.Succeeded)
                {
                    //ApplicationUser user = await _db.Users.FirstOrDefaultAsync(u => u.Email == "admin@gmail.com");
                    //await _userManager.AddToRoleAsync(user, SD.Admin);

                }
                //Add admin role
            }

            return;            

        }
    }
}
