using Microsoft.EntityFrameworkCore;
using WebPersonasCrud.Models.Entidades;

namespace WebPersonasCrud.Data
{
    public class DatosDbContext:DbContext
    {
        public DatosDbContext(DbContextOptions options) : base(options){ }
        public DbSet<PersonasModel> Personas { get; set; }
    }
}
