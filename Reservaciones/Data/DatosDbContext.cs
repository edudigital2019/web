using Microsoft.EntityFrameworkCore;
using Reservaciones.Models.Entidades;
namespace Reservaciones.Data
{
    public class DatosDbContext:DbContext
    {
        public DatosDbContext(DbContextOptions op) : base(op) { }
        public DbSet<ClientesModel> Clientes { get; set; }
        public DbSet<EventosModel> Eventos { get; set; }
        public DbSet<ReservacionesModel> Reservaciones { get; set; }
    }
}
