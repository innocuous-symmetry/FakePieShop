using FakePieShop.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// add services to application
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPieRepository, MockPieRepository>();
builder.Services.AddScoped<ICategoryRepository, MockCategoryRepository>();

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
app.Run();
