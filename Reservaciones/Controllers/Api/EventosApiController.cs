using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservaciones.Data;
using Reservaciones.Models.Entidades;

namespace Reservaciones.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosApiController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public EventosApiController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/EventosApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventosModel>>> GetEventos()
        {
            return await _context.Eventos.ToListAsync();
        }

        // GET: api/EventosApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventosModel>> GetEventosModel(int id)
        {
            var eventosModel = await _context.Eventos.FindAsync(id);

            if (eventosModel == null)
            {
                return NotFound();
            }

            return eventosModel;
        }

        // PUT: api/EventosApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventosModel(int id, EventosModel eventosModel)
        {
            if (id != eventosModel.EventoId)
            {
                return BadRequest();
            }

            _context.Entry(eventosModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventosModelExists(id))
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

        // POST: api/EventosApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventosModel>> PostEventosModel(EventosModel eventosModel)
        {
            _context.Eventos.Add(eventosModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventosModel", new { id = eventosModel.EventoId }, eventosModel);
        }

        // DELETE: api/EventosApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventosModel(int id)
        {
            var eventosModel = await _context.Eventos.FindAsync(id);
            if (eventosModel == null)
            {
                return NotFound();
            }

            _context.Eventos.Remove(eventosModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventosModelExists(int id)
        {
            return _context.Eventos.Any(e => e.EventoId == id);
        }
    }
}
