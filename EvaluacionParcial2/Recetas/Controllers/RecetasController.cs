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
    public class RecetasController : ControllerBase
    {
        private readonly RecetasContext _context;

        public RecetasController(RecetasContext context)
        {
            _context = context;
        }

        // GET: api/Recetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receta>>> Get()
        => await _context.Recetas
            .Include(r => r.RecetaIngredientes)
            .ThenInclude(ri => ri.Ingrediente)
            .AsNoTracking()
            .ToListAsync();

        // GET: api/Recetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receta>> GetReceta(int id)
        {
            var r = await _context.Recetas
                .Include(x => x.RecetaIngredientes)
                .ThenInclude(ri => ri.Ingrediente)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.RecetaId == id);

            return r is null ? NotFound() : r;
        }

        // PUT: api/Recetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceta(int id, Receta receta)
        {
            if (id != receta.RecetaId) return BadRequest();

            _context.Entry(receta).State = EntityState.Modified;
            foreach (var ri in receta.RecetaIngredientes ?? Enumerable.Empty<RecetaIngrediente>())
            {
                _context.Entry(ri).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Recetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Receta receta)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            receta.RecetaId = 0;
            foreach (var ri in receta.RecetaIngredientes ?? Enumerable.Empty<RecetaIngrediente>())
            {
                ri.RecetaId = 0;
                ri.Receta = null;
                ri.Ingrediente = null;
            }

            try
            {
                _context.Recetas.Add(receta);
                await _context.SaveChangesAsync();
                return Created($"/api/recetas/{receta.RecetaId}", new { id = receta.RecetaId });
            }
            catch (DbUpdateException ex)
            {
                return Problem(detail: ex.InnerException?.Message ?? ex.Message, statusCode: 500);
            }
        }


        // DELETE: api/Recetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceta(int id)
        {
            var r = await _context.Recetas.FindAsync(id);
            if (r is null) return NotFound();
            _context.Recetas.Remove(r);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
