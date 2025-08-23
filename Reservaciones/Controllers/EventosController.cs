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
    public class EventosController : Controller
    {
        private readonly DatosDbContext _context;

        public EventosController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eventos.ToListAsync());
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventosModel = await _context.Eventos
                .FirstOrDefaultAsync(m => m.EventoId == id);
            if (eventosModel == null)
            {
                return NotFound();
            }

            return View(eventosModel);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventoId,Nombre,Descripcion,Fecha,Ubicacion")] EventosModel eventosModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventosModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventosModel);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventosModel = await _context.Eventos.FindAsync(id);
            if (eventosModel == null)
            {
                return NotFound();
            }
            return View(eventosModel);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventoId,Nombre,Descripcion,Fecha,Ubicacion")] EventosModel eventosModel)
        {
            if (id != eventosModel.EventoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventosModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventosModelExists(eventosModel.EventoId))
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
            return View(eventosModel);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventosModel = await _context.Eventos
                .FirstOrDefaultAsync(m => m.EventoId == id);
            if (eventosModel == null)
            {
                return NotFound();
            }

            return View(eventosModel);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventosModel = await _context.Eventos.FindAsync(id);
            if (eventosModel != null)
            {
                _context.Eventos.Remove(eventosModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventosModelExists(int id)
        {
            return _context.Eventos.Any(e => e.EventoId == id);
        }
    }
}
