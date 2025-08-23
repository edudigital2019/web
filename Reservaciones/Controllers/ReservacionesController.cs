using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservaciones.Data;
using Reservaciones.Models.Entidades;

namespace Reservaciones.Controllers
{
    public class ReservacionesController : Controller
    {
        private readonly DatosDbContext _context;

        public ReservacionesController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: Reservaciones
        public async Task<IActionResult> Index()
        {
            var datosDbContext = _context.Reservaciones.Include(r => r.Cliente).Include(r => r.Evento);
            return View(await datosDbContext.ToListAsync());
        }

        // GET: Reservaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservacionesModel = await _context.Reservaciones
                .Include(r => r.Cliente)
                .Include(r => r.Evento)
                .FirstOrDefaultAsync(m => m.ReservacionId == id);
            if (reservacionesModel == null)
            {
                return NotFound();
            }

            return View(reservacionesModel);
        }

        // GET: Reservaciones/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Apellido");
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "Descripcion");
            return View();
        }

        // POST: Reservaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservacionId,EventoId,ClienteId,FechaReserva")] ReservacionesModel reservacionesModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservacionesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Apellido", reservacionesModel.ClienteId);
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "Descripcion", reservacionesModel.EventoId);
            return View(reservacionesModel);
        }

        // GET: Reservaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservacionesModel = await _context.Reservaciones.FindAsync(id);
            if (reservacionesModel == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Apellido", reservacionesModel.ClienteId);
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "Descripcion", reservacionesModel.EventoId);
            return View(reservacionesModel);
        }

        // POST: Reservaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservacionId,EventoId,ClienteId,FechaReserva")] ReservacionesModel reservacionesModel)
        {
            if (id != reservacionesModel.ReservacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservacionesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservacionesModelExists(reservacionesModel.ReservacionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Apellido", reservacionesModel.ClienteId);
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "Descripcion", reservacionesModel.EventoId);
            return View(reservacionesModel);
        }

        // GET: Reservaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservacionesModel = await _context.Reservaciones
                .Include(r => r.Cliente)
                .Include(r => r.Evento)
                .FirstOrDefaultAsync(m => m.ReservacionId == id);
            if (reservacionesModel == null)
            {
                return NotFound();
            }

            return View(reservacionesModel);
        }

        // POST: Reservaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservacionesModel = await _context.Reservaciones.FindAsync(id);
            if (reservacionesModel != null)
            {
                _context.Reservaciones.Remove(reservacionesModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservacionesModelExists(int id)
        {
            return _context.Reservaciones.Any(e => e.ReservacionId == id);
        }
    }
}
