using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Factura_Backend.Data;
using Factura_Backend.Models.Entidades;

namespace Factura_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Producto : ControllerBase
    {
        private readonly DatosDBContext _context;

        public Producto(DatosDBContext context)
        {
            _context = context;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductosModel>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductosModel>> GetProductosModel(int id)
        {
            var productosModel = await _context.Productos.FindAsync(id);

            if (productosModel == null)
            {
                return NotFound();
            }

            return productosModel;
        }

        // PUT: api/Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductosModel(int id, ProductosModel productosModel)
        {
            if (id != productosModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(productosModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductosModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductosModel>> PostProductosModel(ProductosModel productosModel)
        {
            _context.Productos.Add(productosModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductosModel", new { id = productosModel.Id }, productosModel);
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductosModel(int id)
        {
            var productosModel = await _context.Productos.FindAsync(id);
            if (productosModel == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(productosModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductosModelExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
