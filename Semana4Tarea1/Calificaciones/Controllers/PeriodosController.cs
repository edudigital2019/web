using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Calificaciones.Data;
using Calificaciones.Models;

namespace Calificaciones.Controllers
{
    public class PeriodosController : Controller
    {
        private readonly DatosDbContext _context;

        public PeriodosController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: Periodos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Periodos.ToListAsync());
        }

        // GET: Periodos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periodosModels = await _context.Periodos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (periodosModels == null)
            {
                return NotFound();
            }

            return View(periodosModels);
        }

        // GET: Periodos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Periodos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,FechaInicio,FechaFin,Activo")] PeriodosModels periodosModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(periodosModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(periodosModels);
        }

        // GET: Periodos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periodosModels = await _context.Periodos.FindAsync(id);
            if (periodosModels == null)
            {
                return NotFound();
            }
            return View(periodosModels);
        }

        // POST: Periodos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,FechaInicio,FechaFin,Activo")] PeriodosModels periodosModels)
        {
            if (id != periodosModels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(periodosModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeriodosModelsExists(periodosModels.Id))
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
            return View(periodosModels);
        }

        // GET: Periodos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periodosModels = await _context.Periodos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (periodosModels == null)
            {
                return NotFound();
            }

            return View(periodosModels);
        }

        // POST: Periodos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var periodosModels = await _context.Periodos.FindAsync(id);
            if (periodosModels != null)
            {
                _context.Periodos.Remove(periodosModels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeriodosModelsExists(int id)
        {
            return _context.Periodos.Any(e => e.Id == id);
        }
    }
}
