using FakePieShop.Models;
using FakePieShop.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// add services to application
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(services => ShoppingCart.GetCart(services));

// include support for sessions and http context
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// use this configuration if using .net for backend only
// builder.Services.AddControllers();

// configuration for MVC and entity framework core
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews() // this does the same as the above, but also includes support for .net views
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<FakePieShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:FakePieShopDbContextConnection"]);
});

var app = builder.Build();

// middlewares -- ORDER MATTERS here
// when including services to the `builder`, order does not matter
app.UseStaticFiles();
app.UseSession();

// dx and debugging
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// if writing a backend only project:
// app.MapControllers();
app.MapDefaultControllerRoute(); // otherwise, this does the same configuration, but full stack
app.MapRazorPages();

// use our own seed data
DbInitializer.Seed(app);

app.Run();
