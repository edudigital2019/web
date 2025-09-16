using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factura_Backend.Models.Entidades
{
    [Table("Facturas")]
    public class FacturasModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [Display(Name = "Fecha de Venta")]
        public DateTime FechaVenta { get; set; }
        [Display(Name = "Codigo de venta")]
        public string Codigo_Venta { get; set; }
        public string Notas { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public double Sub_Total_Venta { get; set; }
        public string Estado_Venta { get; set; }
        public double? Descuento { get; set; }
        public double Total_Venta { get; set; }

        public string Metodo_Pago { get; set; }

        [Display(Name = "ClientesId")]
        [ForeignKey("ClientesModel")]
        public int ClientesModelId { get; set; }
        public ClientesModel ClientesModel { get; set; }
        public ICollection<DetallesFacturaModel> DetalleFactura { get; set; }

        public FacturasModel()
        {
            DetalleFactura = new List<DetallesFacturaModel>();
        }
    }
}
