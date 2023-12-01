using FlowerShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FlowerShop.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FlowerShopDBContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("test"));
    //try to connect to azure DB
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlowerShopDB"));
});

//builder.Services.AddIdentity<User, IdentityRole>()
//    .AddEntityFrameworkStores<AutoDbContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AutoDbContext>();

builder.Services.AddDbContext<AutoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AutoDbContextConnection"));
});

//Identity uses razor pages
builder.Services.AddRazorPages();

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
app.UseAuthentication();;

//used in Identity
app.UseAuthentication();
app.UseAuthorization();


////app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
