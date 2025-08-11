namespace Tipo_Datos.Models.Entidades
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Tipo_Datos.Models.Entidades.Base;

    [Table("Clientes")]
    public class ClientesModel:BaseModel
    {
        [Required(ErrorMessage ="Campo Requerido")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public string Cedula_RUC { get; set; }

    }
}
