using Core.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositorios;
using Microsoft.EntityFrameworkCore;
using Web.Middleware;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPacienteService, PacienteService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Configura el pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pacientes}/{action=Index}/{id?}");


app.UseMiddleware<ExceptionMiddleware>();

//app.MapRazorPages();

app.Run();

