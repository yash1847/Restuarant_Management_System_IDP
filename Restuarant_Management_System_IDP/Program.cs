using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Restuarant_Management_System_IDP.Repository.IRepository;
using Restuarant_Management_System_IDP.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Restuarant_Management_System_IDP.Data;
using Restuarant_Management_System_IDP;
using Restuarant_Management_System_IDP.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//Adding Dbcontext 
//string conString = builder.Configuration.GetConnectionString("sqlcon");
builder.Services.AddDbContext<RestaurantDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("sqlcon"))
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                    {
                        options.Password.RequireDigit = true;
                        options.Password.RequireLowercase = true;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = true;
                        options.Password.RequiredLength = 6;
                        options.Password.RequiredUniqueChars = 1;
                    })
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<RestaurantDbContext>();

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequiredLength = 6;
//});

//Adding UnitofWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); //repository
//builder.Services.AddScoped<IDbInitializer, DbInitializer>(); //dbinitializing
builder.Services.AddHttpContextAccessor();

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout duration
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); //using session services

seedDatabase();

//using authentication and authorization services
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void seedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var scopedServices = scope.ServiceProvider;
        SeedData.Initialize(scopedServices).Wait();
    }
}
