using Microsoft.EntityFrameworkCore;
using ProyectoAngular.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Añadir DB
builder.Services.AddDbContext<DbangularContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("connectionString"));
});

//Añadir CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

//Usar CORS
app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();

//NuGets
//Microsoft.EntityFrameworkCore.Tools
//Microsoft.EntityFrameworkCore.SqlServer

//Comandos
//Scaffold-DbContext "Server=DESKTOP-M0IN372\SQLEXPRESS; DataBase=DBAngular; Trusted_Connection=True; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models
