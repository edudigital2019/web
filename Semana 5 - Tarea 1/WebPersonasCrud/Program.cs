using Microsoft.EntityFrameworkCore;
using WebPersonasCrud.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Especifica los orígenes permitidos
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


var cn = builder.Configuration.GetConnectionString("cn")
    ?? throw new InvalidOperationException("No existe la referencia a la conexion");

builder.Services.AddDbContext<DatosDbContext>(opciones => opciones.UseSqlServer(cn));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS Middleware
app.UseCors("PermitirTodo");

app.UseAuthorization();

app.MapControllers();

app.Run();
