

using Factura_Backend.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var cn = builder.Configuration.GetConnectionString("cn")
    ?? throw new InvalidOperationException("No existe la referencia a la conexion");
builder.Services.AddDbContext<DatosDBContext>(opciones => opciones.UseSqlServer(cn));


builder.Services
    .AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(op => {
    op.AddPolicy("facturacion", credenciales =>
    {
        //credenciales.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        credenciales.WithOrigins("http://localhost:4200") // Especifica los orígenes permitidos
                  .AllowAnyHeader()
                  .AllowAnyMethod();
    });
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("facturacion");      /////////////////////////
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
