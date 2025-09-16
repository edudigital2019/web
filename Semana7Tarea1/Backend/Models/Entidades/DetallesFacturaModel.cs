using Factura_Backend.Models.Entidades;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factura_Backend.Models.Entidades
{
    [Table("DetalleFacturas")]
    public class DetallesFacturaModel
    {
       
        public int Id { get; set; }

        [Required]
        public int ProductosModelId { get; set; }
        public ProductosModel ProductosModel { get; set; }

        public string Nombre { get; set; }

        [Required]
        public double Precio { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public double Monto { get; set; }

        [Display(Name = "Facturas")]
        [ForeignKey("FacturasModel")]
        public int FacturasModelId { get; set; }
        public FacturasModel FacturasModel { get; set; }

    }
}