using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebPersonasCrud.Models.Entidades.Base;

namespace WebPersonasCrud.Models.Entidades
{
    public class PersonasModel : BaseModel
    {
        [Required(ErrorMessage = "Cammpo Requerido")]
        public string Nombres { get; set; }

        [EmailAddress(ErrorMessage = "El formato no de correo electronico")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Cammpo Requerido")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Cammpo Requerido")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Cammpo Requerido")]
        [JsonPropertyName("cedula_ruc")]
        public string Cedula_RUC { get; set; }
    }
}
