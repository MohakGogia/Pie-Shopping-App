using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Models;
using ShoppingApp.Models.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();

// Add services to the container such as adding MVC service to our app
builder.Services
    .AddControllersWithViews()
    .AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<PieShopDBContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection"),
        options => options.CommandTimeout(120)
        )
    );

var app = builder.Build();

// Configure DBContext and apply any pending migrations
using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<PieShopDBContext>();

    var isMigrationPending = dbContext.Database.GetPendingMigrations().Any();

    if (isMigrationPending)
    {
        dbContext.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();
DbInitializer.Seed(app); // Seed the data only when empty
app.Run();
