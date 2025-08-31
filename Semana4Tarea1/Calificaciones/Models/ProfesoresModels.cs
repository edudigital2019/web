using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calificaciones.Models
{
    [Table("Profesores")]
    public class ProfesoresModels
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
        [Phone(ErrorMessage = "Número telefónico no válido")]
        public string? Telefono { get; set; }

        public ICollection<CursosModels> Cursos { get; set; } = new List<CursosModels>();
    }
}
