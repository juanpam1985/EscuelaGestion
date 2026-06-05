using EscuelaGestion.Data;
using EscuelaGestion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EscuelaGestion.Controllers;

public class ClasesController : Controller
{
    private readonly EscuelaDbContext _context;

    public ClasesController(EscuelaDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var clases = _context.Clases.Include(c => c.Profesor).OrderBy(c => c.Nombre);
        return View(await clases.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var clase = await _context.Clases
            .Include(c => c.Profesor)
            .FirstOrDefaultAsync(c => c.ClaseId == id);

        if (clase == null) return NotFound();
        return View(clase);
    }

    public IActionResult Create()
    {
        CargarProfesores();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Clase clase)
    {
        if (ModelState.IsValid)
        {
            _context.Add(clase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        CargarProfesores(clase.ProfesorId);
        return View(clase);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var clase = await _context.Clases.FindAsync(id);
        if (clase == null) return NotFound();

        CargarProfesores(clase.ProfesorId);
        return View(clase);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Clase clase)
    {
        if (id != clase.ClaseId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(clase);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaseExists(clase.ClaseId)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        CargarProfesores(clase.ProfesorId);
        return View(clase);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var clase = await _context.Clases
            .Include(c => c.Profesor)
            .FirstOrDefaultAsync(c => c.ClaseId == id);

        if (clase == null) return NotFound();
        return View(clase);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var clase = await _context.Clases.FindAsync(id);
        if (clase != null)
        {
            _context.Clases.Remove(clase);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private void CargarProfesores(int? seleccionado = null)
    {
        ViewData["ProfesorId"] = new SelectList(
            _context.Profesores.OrderBy(p => p.Apellido).ToList(),
            "ProfesorId",
            "NombreCompleto",
            seleccionado);
    }

    private bool ClaseExists(int id)
    {
        return _context.Clases.Any(c => c.ClaseId == id);
    }
}
