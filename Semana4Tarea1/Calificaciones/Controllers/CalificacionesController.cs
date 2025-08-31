using Calificaciones.Data;
using Calificaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calificaciones.Controllers
{
    public class CalificacionesController : Controller
    {
        private readonly DatosDbContext _context;

        public CalificacionesController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: Calificaciones
        public async Task<IActionResult> Index()
        {
            var datosDbContext = _context.Calificaciones.Include(c => c.Curso).Include(c => c.Estudiante).Include(c => c.Periodo);
            return View(await datosDbContext.ToListAsync());
        }

        // GET: Calificaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacionesModels = await _context.Calificaciones
                .Include(c => c.Curso)
                .Include(c => c.Estudiante)
                .Include(c => c.Periodo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calificacionesModels == null)
            {
                return NotFound();
            }

            return View(calificacionesModels);
        }

        // GET: Calificaciones/Create

        public IActionResult Create()
        {
         
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Apellidos");
            ViewData["PeriodoId"] = new SelectList(_context.Periodos, "Id", "Nombre");

            
            ViewBag.Cursos = _context.Cursos
                .Where(c => c.Activo)
                .OrderBy(c => c.Nombre)
                .Select(c => new { c.Id, c.Nombre, c.Creditos, c.Activo })
                .ToList();

            return View();
        }

        // POST: Calificaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Id,EstudianteId,CursoId,PeriodoId,Nota")] CalificacionesModels calificacionesModels)
{
            if (!ModelState.IsValid)
            {
                PoblarCombos(calificacionesModels);
                return View(calificacionesModels);
            }

            bool yaExiste = await _context.Calificaciones.AnyAsync(c =>
                c.EstudianteId == calificacionesModels.EstudianteId &&
                c.CursoId == calificacionesModels.CursoId &&
                c.PeriodoId == calificacionesModels.PeriodoId);

            if (yaExiste)
            {
                ModelState.AddModelError(string.Empty,
                    "Ya existe una calificación para este estudiante en ese curso y período.");
                PoblarCombos(calificacionesModels);
                return View(calificacionesModels);
            }

            _context.Add(calificacionesModels);

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx &&
                                              (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                
                ModelState.AddModelError(string.Empty,
                    "Registro duplicado: ya existe una calificación para ese Estudiante/Curso/Período.");
                PoblarCombos(calificacionesModels);
                return View(calificacionesModels);
            }
        }

        private void PoblarCombos(CalificacionesModels calificacion)
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre", calificacion.CursoId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Apellidos", calificacion.EstudianteId);
            ViewData["PeriodoId"] = new SelectList(_context.Periodos, "Id", "Nombre", calificacion.PeriodoId);
        }

        // GET: Calificaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var calificacionesModels = await _context.Calificaciones.FindAsync(id);
    if (calificacionesModels == null)
    {
        return NotFound();
    }
    ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre", calificacionesModels.CursoId);
    ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Apellidos", calificacionesModels.EstudianteId);
    ViewData["PeriodoId"] = new SelectList(_context.Periodos, "Id", "Nombre", calificacionesModels.PeriodoId);
    return View(calificacionesModels);
}

// POST: Calificaciones/Edit/5
// To protect from overposting attacks, enable the specific properties you want to bind to.
// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("Id,EstudianteId,CursoId,PeriodoId,Nota")] CalificacionesModels calificacionesModels)
{
    if (id != calificacionesModels.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            _context.Update(calificacionesModels);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CalificacionesModelsExists(calificacionesModels.Id))
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
    ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre", calificacionesModels.CursoId);
    ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Apellidos", calificacionesModels.EstudianteId);
    ViewData["PeriodoId"] = new SelectList(_context.Periodos, "Id", "Nombre", calificacionesModels.PeriodoId);
    return View(calificacionesModels);
}

// GET: Calificaciones/Delete/5
public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var calificacionesModels = await _context.Calificaciones
        .Include(c => c.Curso)
        .Include(c => c.Estudiante)
        .Include(c => c.Periodo)
        .FirstOrDefaultAsync(m => m.Id == id);
    if (calificacionesModels == null)
    {
        return NotFound();
    }

    return View(calificacionesModels);
}

// POST: Calificaciones/Delete/5
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var calificacionesModels = await _context.Calificaciones.FindAsync(id);
    if (calificacionesModels != null)
    {
        _context.Calificaciones.Remove(calificacionesModels);
    }

    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}

private bool CalificacionesModelsExists(int id)
{
    return _context.Calificaciones.Any(e => e.Id == id);
}
}
}
