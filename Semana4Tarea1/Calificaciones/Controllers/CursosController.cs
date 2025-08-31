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
    public class CursosController : Controller
    {
        private readonly DatosDbContext _context;

        public CursosController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: Cursos
        public async Task<IActionResult> Index()
        {
            var datosDbContext = _context.Cursos.Include(c => c.Profesor);
            return View(await datosDbContext.ToListAsync());
        }

        // GET: Cursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursosModels = await _context.Cursos
                .Include(c => c.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cursosModels == null)
            {
                return NotFound();
            }

            return View(cursosModels);
        }

        // GET: Cursos/Create
        public IActionResult Create()
        {
            ViewData["ProfesorId"] = new SelectList(
                _context.Profesores
                    .Select(p => new {
                        p.Id,
                        NombreCompleto = p.Nombres + " " + p.Apellidos
                    }),
                "Id",
                "NombreCompleto"
            );

            return View();
        }

        // POST: Cursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Creditos,ProfesorId,Activo")] CursosModels cursosModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cursosModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellidos", cursosModels.ProfesorId);
            return View(cursosModels);
        }

        // GET: Cursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var curso = await _context.Cursos
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (curso == null) return NotFound();

            ViewData["ProfesorId"] = new SelectList(
                _context.Profesores.Select(p => new {
                    p.Id,
                    NombreCompleto = p.Nombres + " " + p.Apellidos
                }),
                "Id",
                "NombreCompleto",
                curso.ProfesorId   // <-- seleccionado
            );

            return View(curso);
        }


        // POST: Cursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Creditos,ProfesorId,Activo")] CursosModels cursosModels)
        {
            if (id != cursosModels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cursosModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursosModelsExists(cursosModels.Id))
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
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellidos", cursosModels.ProfesorId);
            return View(cursosModels);
        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursosModels = await _context.Cursos
                .Include(c => c.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cursosModels == null)
            {
                return NotFound();
            }

            return View(cursosModels);
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cursosModels = await _context.Cursos.FindAsync(id);
            if (cursosModels != null)
            {
                _context.Cursos.Remove(cursosModels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursosModelsExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}
