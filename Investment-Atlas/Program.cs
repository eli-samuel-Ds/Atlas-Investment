using Application.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ContextApp>(options => 
                                                 options.UseSqlServer(connectionString));

builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<CountryIndicatorService>();
builder.Services.AddScoped<MacroIndicatorService>();
builder.Services.AddScoped<ReturnRateConfigService>();
builder.Services.AddScoped<SimulationMacroIndicatorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
