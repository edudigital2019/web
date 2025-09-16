
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factura_Backend.Models.Entidades
{
    [Table("Clientes")]
    public class ClientesModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public string Cedula_ruc { get; set; }

    }
}
