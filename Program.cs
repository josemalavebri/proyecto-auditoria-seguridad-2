using front_auditoria.Respository.Encuesta;
using front_auditoria.Respository.Lugares;
using Microsoft.EntityFrameworkCore;
using proyecto_auditoria_seguridad.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<EncuestaDBContext>(options =>
    options.UseSqlServer("Server=DESKTOP-0PURE6E;Database=EncuestaDB8;Trusted_Connection=True;TrustServerCertificate=True;"));


builder.Services.AddScoped<IRepositoryGet, RepositoryEncuestaEjecucion>();
builder.Services.AddScoped<IRepositoryDepartamentos, RepositoryDepartamentos>();
builder.Services.AddScoped<IRepositoryFacultades, RepositoryFacultades>();
builder.Services.AddScoped<IRepositoryDirecciones, RepositoryDirecciones>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
