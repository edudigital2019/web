using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recetas.Models
{
    [Table("RecetaIngredientes")]
    [PrimaryKey(nameof(RecetaId), nameof(IngredienteId))]
    public class RecetaIngrediente
    {
        [ForeignKey("Receta")]
        public int RecetaId { get; set; }

        [ForeignKey("Ingrediente")]
        public int IngredienteId { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal Cantidad { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string Unidad { get; set; }

        [Range(0, 10000)]
        public decimal Calorias { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public virtual Receta Receta { get; set; } = null!;

        [JsonIgnore]
        [ValidateNever]
        public virtual Ingrediente Ingrediente { get; set; } = null!;
    }
}