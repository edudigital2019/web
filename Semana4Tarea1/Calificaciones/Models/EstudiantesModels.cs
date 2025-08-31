using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calificaciones.Models
{
    [Table("Estudiantes")]
    [Index(nameof(CodigoMatricula), IsUnique = true)]
    public class EstudiantesModels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string Apellidos { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string CodigoMatricula { get; set; }

        public ICollection<CalificacionesModels> Calificaciones { get; set; } = new List<CalificacionesModels>();
    }
}
