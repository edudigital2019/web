using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calificaciones.Models
{
    [Table("Periodos")]
    public class PeriodosModels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string Nombre { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaFin { get; set; }

        public bool Activo { get; set; }

        public ICollection<CalificacionesModels> Calificaciones { get; set; }=new List<CalificacionesModels>();
    }
}
