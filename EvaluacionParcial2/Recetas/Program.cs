using Microsoft.EntityFrameworkCore;
using Recetas.Data;

var builder = WebApplication.CreateBuilder(args);

//Conexion SQL server
var cn = builder.Configuration.GetConnectionString("cn")
    ?? throw new InvalidOperationException("No existe la referencia a la conexion");
builder.Services.AddDbContext<RecetasContext>(opciones => opciones.UseSqlServer(cn));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(op => {
    op.AddPolicy("receta", credenciales =>
    {
        credenciales.WithOrigins("http://localhost:4200") // Especifica los orígenes permitidos
                  .AllowAnyHeader()
                  .AllowAnyMethod();
    });
});

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("receta");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
