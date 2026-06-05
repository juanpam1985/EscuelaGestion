using EscuelaGestion.Data;
using EscuelaGestion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscuelaGestion.Controllers;

public class EstudiantesController : Controller
{
    private readonly EscuelaDbContext _context;

    public EstudiantesController(EscuelaDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Estudiantes.OrderBy(e => e.Apellido).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var estudiante = await _context.Estudiantes
            .FirstOrDefaultAsync(e => e.EstudianteId == id);

        if (estudiante == null) return NotFound();
        return View(estudiante);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Estudiante estudiante)
    {
        if (ModelState.IsValid)
        {
            _context.Add(estudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(estudiante);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante == null) return NotFound();
        return View(estudiante);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Estudiante estudiante)
    {
        if (id != estudiante.EstudianteId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(estudiante);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudianteExists(estudiante.EstudianteId)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(estudiante);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var estudiante = await _context.Estudiantes
            .FirstOrDefaultAsync(e => e.EstudianteId == id);

        if (estudiante == null) return NotFound();
        return View(estudiante);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante != null)
        {
            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool EstudianteExists(int id)
    {
        return _context.Estudiantes.Any(e => e.EstudianteId == id);
    }
}
