using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Recetas.Models
{
    [Table("Ingredientes")]
    public class Ingrediente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredienteId { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string Nombre { get; set; }

        [JsonIgnore]
        public virtual ICollection<RecetaIngrediente> RecetaIngredientes { get; set; } = new List<RecetaIngrediente>();
    }
}
