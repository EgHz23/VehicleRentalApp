using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using VehicleRentalApp.Data;
using VehicleRentalApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Dodajte povezavo do baze
var connectionString = builder.Configuration.GetConnectionString("VehicleRentalContext");
builder.Services.AddDbContext<VehicleRentalContext>(options =>
    options.UseSqlServer(connectionString));

// Dodajte Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<VehicleRentalContext>();

builder.Services.AddControllersWithViews();


var app = builder.Build();

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Privzete poti
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
