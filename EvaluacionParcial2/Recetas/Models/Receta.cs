using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recetas.Models
{
    [Table("Recetas")]
    public class Receta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecetaId { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public int TiempoPreparacion { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string Dificultad { get; set; }

        public virtual ICollection<RecetaIngrediente> RecetaIngredientes { get; set; } = new List<RecetaIngrediente>();
    }
}
