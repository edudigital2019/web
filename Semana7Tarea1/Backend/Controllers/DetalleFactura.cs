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
    public class DetalleFactura : ControllerBase
    {
        private readonly DatosDBContext _context;

        public DetalleFactura(DatosDBContext context)
        {
            _context = context;
        }

        // GET: api/DetalleFactura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallesFacturaModel>>> GetDetalleFactura()
        {
            return await _context.DetalleFactura.ToListAsync();
        }

        // GET: api/DetalleFactura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallesFacturaModel>> GetDetallesFacturaModel(int id)
        {
            var detallesFacturaModel = await _context.DetalleFactura.FindAsync(id);

            if (detallesFacturaModel == null)
            {
                return NotFound();
            }

            return detallesFacturaModel;
        }

        // PUT: api/DetalleFactura/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallesFacturaModel(int id, DetallesFacturaModel detallesFacturaModel)
        {
            if (id != detallesFacturaModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(detallesFacturaModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallesFacturaModelExists(id))
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

        // POST: api/DetalleFactura
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetallesFacturaModel>> PostDetallesFacturaModel(DetallesFacturaModel detallesFacturaModel)
        {
            _context.DetalleFactura.Add(detallesFacturaModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetallesFacturaModel", new { id = detallesFacturaModel.Id }, detallesFacturaModel);
        }

        // DELETE: api/DetalleFactura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallesFacturaModel(int id)
        {
            var detallesFacturaModel = await _context.DetalleFactura.FindAsync(id);
            if (detallesFacturaModel == null)
            {
                return NotFound();
            }

            _context.DetalleFactura.Remove(detallesFacturaModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetallesFacturaModelExists(int id)
        {
            return _context.DetalleFactura.Any(e => e.Id == id);
        }
    }
}
