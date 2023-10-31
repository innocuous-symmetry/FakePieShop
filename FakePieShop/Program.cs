using BethanysPieShop.Models;
using FakePieShop.Models;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// add services to application
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FakePieShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:FakePieShopDbContextConnection"]);
});

WebApplication app = builder.Build();

// middlewares
app.UseStaticFiles();
//app.UseAuthentication();

// dx and debugging
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute();

// use our own seed data
DbInitializer.Seed(app);

app.Run();
