using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calificaciones.Models
{
    [Table("Cursos")]
    public class CursosModels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string Nombre { get; set; }

        [Range(0, 30)]
        public int Creditos { get; set; }

        [ForeignKey(nameof(ProfesorId))]
        [Display(Name = "ProfesoresMoldels")]
        public int ProfesorId { get; set; }

        public bool Activo { get; set; }

        public ProfesoresModels? Profesor { get; set; }

        public ICollection<CalificacionesModels> Calificaciones { get; set; } = new List<CalificacionesModels>();
    }
}
