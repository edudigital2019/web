using Calificaciones.Models;
using Microsoft.EntityFrameworkCore;

namespace Calificaciones.Data
{
    public class DatosDbContext:DbContext
    {
        public DatosDbContext(DbContextOptions op) : base(op) { }
        public DbSet<EstudiantesModels> Estudiantes { get; set; }
        public DbSet<ProfesoresModels> Profesores { get; set; }
        public DbSet<CursosModels>Cursos { get; set; }  
        public DbSet<PeriodosModels> Periodos { get; set; }
        public DbSet<CalificacionesModels>Calificaciones { get; set; }

    }
}
