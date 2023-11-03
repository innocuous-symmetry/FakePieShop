using FakePieShop.Models;
using FakePieShop.Repositories;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// add services to application
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(services => ShoppingCart.GetCart(services));

// include support for sessions and http context
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// configuration for MVC and entity framework core
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
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

app.MapDefaultControllerRoute();
app.MapRazorPages();

// use our own seed data
DbInitializer.Seed(app);

app.Run();
