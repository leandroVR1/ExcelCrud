// Controllers/InscripcionesController.cs
using AutoMapper;
using ExcelCrudMVC.Data;
using ExcelCrudMVC.Models;
using ExcelCrudMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class InscripcionesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public InscripcionesController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
{
    var inscripciones = await _context.Inscripciones
        .Include(i => i.Estudiante)
        .Include(i => i.Materia)
        .Include(i => i.Profesor)
        .Include(i => i.Decano)
        .Include(i => i.Universidad)
        .Include(i => i.Carrera)
        .ToListAsync();

    var inscripcionViewModels = inscripciones.Select(i => new InscripcionViewModel
    {
        InscripcionID = i.InscripcionID,
        EstudianteID = i.EstudianteID,
        EstudianteNombre = i.Estudiante.Nombre,
        MateriaID = i.MateriaID,
        MateriaNombre = i.Materia.Nombre,
        ProfesorID = i.ProfesorID,
        ProfesorNombre = i.Profesor.Nombre,
        DecanoID = i.DecanoID,
        DecanoNombre = i.Decano.Nombre,
        UniversidadID = i.UniversidadID,
        UniversidadNombre = i.Universidad.Nombre,
        CarreraID = i.CarreraID,
        CarreraNombre = i.Carrera.Nombre,
        Semestre = i.Semestre,
        Año = i.Año,
        EstadoDeInscripcion = i.EstadoDeInscripcion
    }).ToList();

    return View(inscripcionViewModels);
}


    public async Task<IActionResult> Create()
    {
        var viewModel = new InscripcionViewModel
        {
            Estudiantes = await _context.Estudiantes.ToListAsync(),
            Materias = await _context.Materias.ToListAsync(),
            Profesores = await _context.Profesores.ToListAsync(),
            Decanos = await _context.Decanos.ToListAsync(),
            Universidades = await _context.Universidades.ToListAsync(),
            Carreras = await _context.Carreras.ToListAsync()
        };

        return View(viewModel);
    }

      [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InscripcionViewModel inscripcionViewModel)
    {
        if (ModelState.IsValid)
        {
            var inscripcion = _mapper.Map<Inscripcion>(inscripcionViewModel);
            _context.Add(inscripcion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // If model state is not valid, reload the lists to avoid null references in the view
        inscripcionViewModel.Estudiantes = await _context.Estudiantes.ToListAsync();
        inscripcionViewModel.Materias = await _context.Materias.ToListAsync();
        inscripcionViewModel.Profesores = await _context.Profesores.ToListAsync();
        inscripcionViewModel.Decanos = await _context.Decanos.ToListAsync();
        inscripcionViewModel.Universidades = await _context.Universidades.ToListAsync();
        inscripcionViewModel.Carreras = await _context.Carreras.ToListAsync();

        return View(inscripcionViewModel);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var inscripcion = await _context.Inscripciones.FindAsync(id);
        if (inscripcion == null)
        {
            return NotFound();
        }
        var inscripcionViewModel = _mapper.Map<InscripcionViewModel>(inscripcion);
        return View(inscripcionViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, InscripcionViewModel inscripcionViewModel)
    {
        if (id != inscripcionViewModel.InscripcionID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var inscripcion = _mapper.Map<Inscripcion>(inscripcionViewModel);
                _context.Update(inscripcion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscripcionExists(inscripcionViewModel.InscripcionID))
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
        return View(inscripcionViewModel);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var inscripcion = await _context.Inscripciones
            .FirstOrDefaultAsync(m => m.InscripcionID == id);
        if (inscripcion == null)
        {
            return NotFound();
        }

        var inscripcionViewModel = _mapper.Map<InscripcionViewModel>(inscripcion);
        return View(inscripcionViewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var inscripcion = await _context.Inscripciones.FindAsync(id);
        _context.Inscripciones.Remove(inscripcion);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool InscripcionExists(int id)
    {
        return _context.Inscripciones.Any(e => e.InscripcionID == id);
    }
}
