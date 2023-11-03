using BethanysPieShop.Models;
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
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FakePieShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:FakePieShopDbContextConnection"]);
});

var app = builder.Build();

// middlewares
app.UseStaticFiles();
app.UseSession();

// dx and debugging
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute();

// use our own seed data
DbInitializer.Seed(app);

app.Run();
