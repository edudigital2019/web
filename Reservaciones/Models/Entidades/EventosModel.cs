using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reservaciones.Models.Entidades
{
    [Table ("Eventos")]
    public class EventosModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventoId { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo es requerido")]
        public string? Descripcion { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string Ubicacion { get; set; } = null!;

        public ICollection<ReservacionesModel> Reservaciones { get; set; }

        public EventosModel()
        {
            Reservaciones = new List<ReservacionesModel>();
        }
    }
}
