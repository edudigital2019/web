using Recetas.Models;
using Microsoft.EntityFrameworkCore;

namespace Recetas.Data
{
    public class RecetasContext : DbContext
    {
        public RecetasContext(DbContextOptions<RecetasContext> options) : base(options)
        {
        }

        public DbSet<Receta> Recetas { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<RecetaIngrediente> RecetaIngredientes { get; set; }
    }
}
