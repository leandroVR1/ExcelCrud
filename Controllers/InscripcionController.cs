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
            .Select(i => new InscripcionListViewModel
            {
                InscripcionID = i.InscripcionID,
                EstudianteNombre = i.Estudiante.Nombre,
                MateriaNombre = i.Materia.Nombre,
                ProfesorNombre = i.Profesor.Nombre,
                DecanoNombre = i.Decano.Nombre,
                UniversidadNombre = i.Universidad.Nombre,
                CarreraNombre = i.Carrera.Nombre,
                Semestre = i.Semestre,
                Año = i.Año,
                EstadoDeInscripcion = i.EstadoDeInscripcion
            })
            .ToListAsync();

        return View(inscripciones);
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

        // Si el estado del modelo no es válido, recarga las listas para evitar referencias nulas en la vista
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

        var inscripcion = await _context.Inscripciones
            .Include(i => i.Estudiante)
            .Include(i => i.Materia)
            .Include(i => i.Profesor)
            .Include(i => i.Decano)
            .Include(i => i.Universidad)
            .Include(i => i.Carrera)
            .FirstOrDefaultAsync(i => i.InscripcionID == id);

        if (inscripcion == null)
        {
            return NotFound();
        }

        var viewModel = new InscripcionViewModel
        {
            InscripcionID = inscripcion.InscripcionID,
            EstudianteID = inscripcion.EstudianteID,
            MateriaID = inscripcion.MateriaID,
            ProfesorID = inscripcion.ProfesorID,
            DecanoID = inscripcion.DecanoID,
            UniversidadID = inscripcion.UniversidadID,
            CarreraID = inscripcion.CarreraID,
            Semestre = inscripcion.Semestre,
            Año = inscripcion.Año,
            EstadoDeInscripcion = inscripcion.EstadoDeInscripcion,

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

        // Recargar las listas en caso de que el estado del modelo no sea válido
        inscripcionViewModel.Estudiantes = await _context.Estudiantes.ToListAsync();
        inscripcionViewModel.Materias = await _context.Materias.ToListAsync();
        inscripcionViewModel.Profesores = await _context.Profesores.ToListAsync();
        inscripcionViewModel.Decanos = await _context.Decanos.ToListAsync();
        inscripcionViewModel.Universidades = await _context.Universidades.ToListAsync();
        inscripcionViewModel.Carreras = await _context.Carreras.ToListAsync();

        return View(inscripcionViewModel);
    }


    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var inscripcion = await _context.Inscripciones
            .Include(i => i.Estudiante)
            .Include(i => i.Materia)
            .Include(i => i.Profesor)
            .Include(i => i.Decano)
            .Include(i => i.Universidad)
            .Include(i => i.Carrera)
            .FirstOrDefaultAsync(m => m.InscripcionID == id);

        if (inscripcion == null)
        {
            return NotFound();
        }

        var viewModel = new InscripcionListViewModel
        {
            InscripcionID = inscripcion.InscripcionID,
            EstudianteNombre = inscripcion.Estudiante.Nombre,
            MateriaNombre = inscripcion.Materia.Nombre,
            ProfesorNombre = inscripcion.Profesor.Nombre,
            DecanoNombre = inscripcion.Decano.Nombre,
            UniversidadNombre = inscripcion.Universidad.Nombre,
            CarreraNombre = inscripcion.Carrera.Nombre,
            Semestre = inscripcion.Semestre,
            Año = inscripcion.Año,
            EstadoDeInscripcion = inscripcion.EstadoDeInscripcion
        };

        return View(viewModel);
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
