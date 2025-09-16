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
    public class Cliente : ControllerBase
    {
        private readonly DatosDBContext _context;

        public Cliente(DatosDBContext context)
        {
            _context = context;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesModel>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientesModel>> GetClientesModel(int id)
        {
            var clientesModel = await _context.Clientes.FindAsync(id);

            if (clientesModel == null)
            {
                return NotFound();
            }

            return clientesModel;
        }

        // PUT: api/Cliente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientesModel(int id, ClientesModel clientesModel)
        {
            if (id != clientesModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesModelExists(id))
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

        // POST: api/Cliente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientesModel>> PostClientesModel(ClientesModel clientesModel)
        {
            _context.Clientes.Add(clientesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientesModel", new { id = clientesModel.Id }, clientesModel);
        }

        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientesModel(int id)
        {
            var clientesModel = await _context.Clientes.FindAsync(id);
            if (clientesModel == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(clientesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientesModelExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
