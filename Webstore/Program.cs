using Microsoft.EntityFrameworkCore;
using Webstore.DAL;
using Webstore.Service.Interfaces;
using Webstore.Service.Implementations;
using Webstore.DAL.Interfaces;
using Webstore.Domain.Entity;
using Webstore.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/User/Login");
        options.AccessDeniedPath = new PathString("/User/Login");
    });
builder.Services.AddDistributedMemoryCache();

builder.Services.AddMemoryCache();



builder.Services.AddScoped<IBaseRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=ReadProducts}/{id?}");



app.Run();
