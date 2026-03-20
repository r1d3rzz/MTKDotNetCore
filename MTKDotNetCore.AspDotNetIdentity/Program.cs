using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.AspDotNetIdentity.Data;
using MTKDotNetCore.AspDotNetIdentity.Models;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("UsingIdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'MTKDotNetCoreAspDotNetIdentityContextConnection' not found.");;

builder.Services.AddDbContext<MTKDotNetCoreAspDotNetIdentityContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<UsingIdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MTKDotNetCoreAspDotNetIdentityContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
