using EscuelaGestion.Data;
using EscuelaGestion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscuelaGestion.Controllers;

public class ProfesoresController : Controller
{
    private readonly EscuelaDbContext _context;

    public ProfesoresController(EscuelaDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Profesores.OrderBy(p => p.Apellido).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var profesor = await _context.Profesores
            .FirstOrDefaultAsync(p => p.ProfesorId == id);

        if (profesor == null) return NotFound();
        return View(profesor);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Profesor profesor)
    {
        if (ModelState.IsValid)
        {
            _context.Add(profesor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(profesor);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var profesor = await _context.Profesores.FindAsync(id);
        if (profesor == null) return NotFound();
        return View(profesor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Profesor profesor)
    {
        if (id != profesor.ProfesorId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(profesor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorExists(profesor.ProfesorId)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(profesor);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var profesor = await _context.Profesores
            .FirstOrDefaultAsync(p => p.ProfesorId == id);

        if (profesor == null) return NotFound();
        return View(profesor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var profesor = await _context.Profesores.FindAsync(id);
        if (profesor != null)
        {
            _context.Profesores.Remove(profesor);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool ProfesorExists(int id)
    {
        return _context.Profesores.Any(p => p.ProfesorId == id);
    }
}
