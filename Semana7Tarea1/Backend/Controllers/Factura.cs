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
    public class Factura : ControllerBase
    {
        private readonly DatosDBContext _context;

        public Factura(DatosDBContext context)
        {
            _context = context;
        }

        // GET: api/Factura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturasModel>>> GetFacturas()
        {
            return await _context.Facturas.ToListAsync();
        }

        // GET: api/Factura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacturasModel>> GetFacturasModel(int id)
        {
            var facturasModel = await _context.Facturas.FindAsync(id);

            if (facturasModel == null)
            {
                return NotFound();
            }

            return facturasModel;
        }

        // PUT: api/Factura/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacturasModel(int id, FacturasModel facturasModel)
        {
            if (id != facturasModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(facturasModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturasModelExists(id))
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

        // POST: api/Factura
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FacturasModel>> PostFacturasModel(FacturasModel facturasModel)
        {
            _context.Facturas.Add(facturasModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacturasModel", new { id = facturasModel.Id }, facturasModel);
        }

        // DELETE: api/Factura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacturasModel(int id)
        {
            var facturasModel = await _context.Facturas.FindAsync(id);
            if (facturasModel == null)
            {
                return NotFound();
            }

            _context.Facturas.Remove(facturasModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacturasModelExists(int id)
        {
            return _context.Facturas.Any(e => e.Id == id);
        }
    }
}
