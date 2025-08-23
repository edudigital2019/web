using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reservaciones.Models.Entidades
{
    public class ReservacionesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservacionId { get; set; }

        [Display(Name = "EventoId")]
        [ForeignKey("EventosModel")]
        public int EventoId { get; set; }

        [Display(Name = "ClienteId")]
        [ForeignKey("ClientesModel")]
        public int ClienteId { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime FechaReserva { get; set; }

        public EventosModel? Evento { get; set; }
        public ClientesModel? Cliente { get; set; }
    }
}
