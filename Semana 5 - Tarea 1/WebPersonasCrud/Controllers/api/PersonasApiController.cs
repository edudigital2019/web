using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPersonasCrud.Data;
using WebPersonasCrud.Models.Entidades;

namespace WebPersonasCrud.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasApiController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public PersonasApiController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonasModel>>> GetPersonas()
        {
            return await _context.Personas.ToListAsync();
        }

        // GET: api/PersonasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonasModel>> GetpersonasModel(int id)
        {
            var personasModel = await _context.Personas.FindAsync(id);

            if (personasModel == null)
            {
                return NotFound();
            }

            return personasModel;
        }

        // PUT: api/PersonasApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutpersonasModel(int id, PersonasModel personasModel)
        {
            if (id != personasModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(personasModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!personasModelExists(id))
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

        // POST: api/PersonasApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonasModel>> PostpersonasModel(PersonasModel personasModel)
        {
            _context.Personas.Add(personasModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetpersonasModel", new { id = personasModel.Id }, personasModel);
        }

        // DELETE: api/PersonasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletepersonasModel(int id)
        {
            var personasModel = await _context.Personas.FindAsync(id);
            if (personasModel == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(personasModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool personasModelExists(int id)
        {
            return _context.Personas.Any(e => e.Id == id);
        }
    }
}
