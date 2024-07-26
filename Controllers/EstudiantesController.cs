// Controllers/EstudiantesController.cs
using AutoMapper;
using ExcelCrudMVC.Data;
using ExcelCrudMVC.Models;
using ExcelCrudMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ExcelCrudMVC.Profiles;

public class EstudiantesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EstudiantesController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var estudiantes = await _context.Estudiantes.ToListAsync();
        var estudianteViewModels = _mapper.Map<List<EstudianteViewModel>>(estudiantes);
        return View(estudianteViewModels);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EstudianteViewModel estudianteViewModel)
    {
        if (ModelState.IsValid)
        {
            var estudiante = _mapper.Map<Estudiante>(estudianteViewModel);
            _context.Add(estudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(estudianteViewModel);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante == null)
        {
            return NotFound();
        }
        var estudianteViewModel = _mapper.Map<EstudianteViewModel>(estudiante);
        return View(estudianteViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EstudianteViewModel estudianteViewModel)
    {
        if (id != estudianteViewModel.EstudianteID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var estudiante = _mapper.Map<Estudiante>(estudianteViewModel);
                _context.Update(estudiante);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudianteExists(estudianteViewModel.EstudianteID))
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
        return View(estudianteViewModel);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var estudiante = await _context.Estudiantes
            .FirstOrDefaultAsync(m => m.EstudianteID == id);
        if (estudiante == null)
        {
            return NotFound();
        }

        var estudianteViewModel = _mapper.Map<EstudianteViewModel>(estudiante);
        return View(estudianteViewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        _context.Estudiantes.Remove(estudiante);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EstudianteExists(int id)
    {
        return _context.Estudiantes.Any(e => e.EstudianteID == id);
    }
}
