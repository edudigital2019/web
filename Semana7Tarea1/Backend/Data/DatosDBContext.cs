using Factura_Backend.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Factura_Backend.Data
{
    public class DatosDBContext : DbContext
    {
        public DatosDBContext(DbContextOptions op) : base(op) { }
        public DbSet<ClientesModel> Clientes { get; set; }
        public DbSet<ProductosModel> Productos { get; set; }
        public DbSet<FacturasModel> Facturas { get; set; }
        public DbSet<DetallesFacturaModel> DetalleFactura { get; set; }       
    }
}
