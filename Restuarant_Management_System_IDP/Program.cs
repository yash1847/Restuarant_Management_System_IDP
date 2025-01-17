using Microsoft.EntityFrameworkCore;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Repository.IRepository;
using Restuarant_Management_System_IDP.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Adding Dbcontext 
//string conString = builder.Configuration.GetConnectionString("sqlcon");
builder.Services.AddDbContext<RestaurantDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("sqlcon"))
);

//Adding UnitofWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); //repository

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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
