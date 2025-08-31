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
    public class ProfesoresController : Controller
    {
        private readonly DatosDbContext _context;

        public ProfesoresController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: Profesores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profesores.ToListAsync());
        }

        // GET: Profesores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesoresModels = await _context.Profesores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesoresModels == null)
            {
                return NotFound();
            }

            return View(profesoresModels);
        }

        // GET: Profesores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombres,Apellidos,Email,Telefono")] ProfesoresModels profesoresModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profesoresModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profesoresModels);
        }

        // GET: Profesores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesoresModels = await _context.Profesores.FindAsync(id);
            if (profesoresModels == null)
            {
                return NotFound();
            }
            return View(profesoresModels);
        }

        // POST: Profesores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombres,Apellidos,Email,Telefono")] ProfesoresModels profesoresModels)
        {
            if (id != profesoresModels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesoresModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesoresModelsExists(profesoresModels.Id))
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
            return View(profesoresModels);
        }

        // GET: Profesores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesoresModels = await _context.Profesores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesoresModels == null)
            {
                return NotFound();
            }

            return View(profesoresModels);
        }

        // POST: Profesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesoresModels = await _context.Profesores.FindAsync(id);
            if (profesoresModels != null)
            {
                _context.Profesores.Remove(profesoresModels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesoresModelsExists(int id)
        {
            return _context.Profesores.Any(e => e.Id == id);
        }
    }
}
