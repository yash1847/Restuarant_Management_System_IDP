using Microsoft.AspNetCore.Identity;
using Restuarant_Management_System_IDP.Models;

namespace Restuarant_Management_System_IDP
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedProvider = scope.ServiceProvider;
                var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();

                // Check and create roles
                if (!await roleManager.RoleExistsAsync(SD.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(SD.Admin));
                }
                if (!await roleManager.RoleExistsAsync(SD.Customer))
                {
                    await roleManager.CreateAsync(new IdentityRole(SD.Customer));
                }
                if(!await roleManager.RoleExistsAsync(SD.Kitchen))
                {
                    await roleManager.CreateAsync(new IdentityRole(SD.Kitchen));
                }
                if (!await roleManager.RoleExistsAsync(SD.Delivery))
                {
                    await roleManager.CreateAsync(new IdentityRole(SD.Delivery));
                }

                // Check and create admin
                var adminEmail = "admin@gmail.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = "admin",
                        FullName = "Admin",
                        Email = adminEmail,
                        PhoneNumber = "9090909090",
                    };

                    string adminPassword = "Admin@123";


                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, SD.Admin);
                    }
                }
                
            }
        }
    }
}
