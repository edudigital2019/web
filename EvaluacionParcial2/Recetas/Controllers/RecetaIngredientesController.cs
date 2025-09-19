using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recetas.Data;
using Recetas.Models;

namespace Recetas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaIngredientesController : ControllerBase
    {
        private readonly RecetasContext _context;

        public RecetaIngredientesController(RecetasContext context)
        {
            _context = context;
        }

        // GET: api/RecetaIngredientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecetaIngrediente>>> GetRecetaIngredientes()
        {
            return await _context.RecetaIngredientes.ToListAsync();
        }

        // GET: api/RecetaIngredientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecetaIngrediente>> GetRecetaIngrediente(int id)
        {
            var recetaIngrediente = await _context.RecetaIngredientes.FindAsync(id);

            if (recetaIngrediente == null)
            {
                return NotFound();
            }

            return recetaIngrediente;
        }

        // PUT: api/RecetaIngredientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecetaIngrediente(int id, RecetaIngrediente recetaIngrediente)
        {
            if (id != recetaIngrediente.RecetaId)
            {
                return BadRequest();
            }

            _context.Entry(recetaIngrediente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecetaIngredienteExists(id))
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

        // POST: api/RecetaIngredientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecetaIngrediente>> PostRecetaIngrediente(RecetaIngrediente recetaIngrediente)
        {
            _context.RecetaIngredientes.Add(recetaIngrediente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RecetaIngredienteExists(recetaIngrediente.RecetaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRecetaIngrediente", new { id = recetaIngrediente.RecetaId }, recetaIngrediente);
        }

        // DELETE: api/RecetaIngredientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecetaIngrediente(int id)
        {
            var recetaIngrediente = await _context.RecetaIngredientes.FindAsync(id);
            if (recetaIngrediente == null)
            {
                return NotFound();
            }

            _context.RecetaIngredientes.Remove(recetaIngrediente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecetaIngredienteExists(int id)
        {
            return _context.RecetaIngredientes.Any(e => e.RecetaId == id);
        }
    }
}
