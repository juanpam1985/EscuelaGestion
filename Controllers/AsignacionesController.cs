using EscuelaGestion.Data;
using EscuelaGestion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EscuelaGestion.Controllers;

public class AsignacionesController : Controller
{
    private readonly EscuelaDbContext _context;

    public AsignacionesController(EscuelaDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var asignaciones = _context.AsignacionesClases
            .Include(a => a.Estudiante)
            .Include(a => a.Clase)
            .ThenInclude(c => c!.Profesor)
            .OrderByDescending(a => a.FechaAsignacion);

        return View(await asignaciones.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var asignacion = await _context.AsignacionesClases
            .Include(a => a.Estudiante)
            .Include(a => a.Clase)
            .FirstOrDefaultAsync(a => a.AsignacionId == id);

        if (asignacion == null) return NotFound();
        return View(asignacion);
    }

    public IActionResult Create()
    {
        CargarListas();
        return View(new AsignacionClase { FechaAsignacion = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AsignacionClase asignacion)
    {
        if (ModelState.IsValid)
        {
            _context.Add(asignacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        CargarListas(asignacion.EstudianteId, asignacion.ClaseId);
        return View(asignacion);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var asignacion = await _context.AsignacionesClases.FindAsync(id);
        if (asignacion == null) return NotFound();

        CargarListas(asignacion.EstudianteId, asignacion.ClaseId);
        return View(asignacion);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AsignacionClase asignacion)
    {
        if (id != asignacion.AsignacionId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(asignacion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignacionExists(asignacion.AsignacionId)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        CargarListas(asignacion.EstudianteId, asignacion.ClaseId);
        return View(asignacion);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var asignacion = await _context.AsignacionesClases
            .Include(a => a.Estudiante)
            .Include(a => a.Clase)
            .FirstOrDefaultAsync(a => a.AsignacionId == id);

        if (asignacion == null) return NotFound();
        return View(asignacion);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var asignacion = await _context.AsignacionesClases.FindAsync(id);
        if (asignacion != null)
        {
            _context.AsignacionesClases.Remove(asignacion);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private void CargarListas(int? estudianteId = null, int? claseId = null)
    {
        ViewData["EstudianteId"] = new SelectList(
            _context.Estudiantes.OrderBy(e => e.Apellido).ToList(),
            "EstudianteId",
            "NombreCompleto",
            estudianteId);

        ViewData["ClaseId"] = new SelectList(
            _context.Clases.OrderBy(c => c.Nombre).ToList(),
            "ClaseId",
            "Nombre",
            claseId);
    }

    private bool AsignacionExists(int id)
    {
        return _context.AsignacionesClases.Any(a => a.AsignacionId == id);
    }
}
