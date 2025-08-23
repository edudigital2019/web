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
    public class ReservacionesApiController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public ReservacionesApiController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/ReservacionesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservacionesModel>>> GetReservaciones()
        {
            return await _context.Reservaciones.ToListAsync();
        }

        // GET: api/ReservacionesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservacionesModel>> GetReservacionesModel(int id)
        {
            var reservacionesModel = await _context.Reservaciones.FindAsync(id);

            if (reservacionesModel == null)
            {
                return NotFound();
            }

            return reservacionesModel;
        }

        // PUT: api/ReservacionesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservacionesModel(int id, ReservacionesModel reservacionesModel)
        {
            if (id != reservacionesModel.ReservacionId)
            {
                return BadRequest();
            }

            _context.Entry(reservacionesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservacionesModelExists(id))
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

        // POST: api/ReservacionesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReservacionesModel>> PostReservacionesModel(ReservacionesModel reservacionesModel)
        {
            _context.Reservaciones.Add(reservacionesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservacionesModel", new { id = reservacionesModel.ReservacionId }, reservacionesModel);
        }

        // DELETE: api/ReservacionesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservacionesModel(int id)
        {
            var reservacionesModel = await _context.Reservaciones.FindAsync(id);
            if (reservacionesModel == null)
            {
                return NotFound();
            }

            _context.Reservaciones.Remove(reservacionesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservacionesModelExists(int id)
        {
            return _context.Reservaciones.Any(e => e.ReservacionId == id);
        }
    }
}
