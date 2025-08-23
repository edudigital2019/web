using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reservaciones.Models.Entidades
{
    public class ClientesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo es requerido")]
        public string Apellido { get; set; } = null!;

        [EmailAddress(ErrorMessage = "El formato no de correo electronico")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "El campo es requerido")]
        public string? Telefono { get; set; }

        public ICollection<ReservacionesModel> Reservaciones { get; set; } 

        public ClientesModel()
        {
            Reservaciones = new List<ReservacionesModel>();
        }
    }
}
