using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ExcelCrudMVC.Data;
using ExcelCrudMVC.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[Authorize(Policy = "AdminPolicy")]
public class ExportController : Controller
{
  private readonly ApplicationDbContext _context;

  public ExportController(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<IActionResult> ExportToExcel()
  {
    var inscripciones = await _context.Inscripciones
        .Include(i => i.Estudiante)
        .Include(i => i.Materia)
        .Include(i => i.Profesor)
        .Include(i => i.Decano)
        .Include(i => i.Universidad)
        .Include(i => i.Carrera)
        .ToListAsync();

    using (var package = new ExcelPackage())
    {
      var worksheet = package.Workbook.Worksheets.Add("Inscripciones");

      // Encabezados
      worksheet.Cells[1, 1].Value = "ID";
      worksheet.Cells[1, 2].Value = "Nombre Estudiante";
      worksheet.Cells[1, 3].Value = "Apellido Estudiante";
      worksheet.Cells[1, 4].Value = "Correo Estudiante";
      worksheet.Cells[1, 5].Value = "Teléfono Estudiante";
      worksheet.Cells[1, 6].Value = "Nombre Materia";
      worksheet.Cells[1, 7].Value = "Semestre";
      worksheet.Cells[1, 8].Value = "Año";
      worksheet.Cells[1, 9].Value = "Nombre Profesor";
      worksheet.Cells[1, 10].Value = "Apellido Profesor";
      worksheet.Cells[1, 11].Value = "Correo Profesor";
      worksheet.Cells[1, 12].Value = "Teléfono Profesor";
      worksheet.Cells[1, 13].Value = "Decano Universidad";
      worksheet.Cells[1, 14].Value = "Carrera";
      worksheet.Cells[1, 15].Value = "Universidad";
      worksheet.Cells[1, 16].Value = "Estado de Inscripción";

      // Datos
      for (int i = 0; i < inscripciones.Count; i++)
      {
        var inscripcion = inscripciones[i];
        worksheet.Cells[i + 2, 1].Value = inscripcion.InscripcionID;
        worksheet.Cells[i + 2, 2].Value = inscripcion.Estudiante.Nombre;
        worksheet.Cells[i + 2, 3].Value = inscripcion.Estudiante.Apellido;
        worksheet.Cells[i + 2, 4].Value = inscripcion.Estudiante.Correo;
        worksheet.Cells[i + 2, 5].Value = inscripcion.Estudiante.Telefono;
        worksheet.Cells[i + 2, 6].Value = inscripcion.Materia.Nombre;
        worksheet.Cells[i + 2, 7].Value = inscripcion.Semestre;
        worksheet.Cells[i + 2, 8].Value = inscripcion.Año;
        worksheet.Cells[i + 2, 9].Value = inscripcion.Profesor.Nombre;
        worksheet.Cells[i + 2, 10].Value = inscripcion.Profesor.Apellido;
        worksheet.Cells[i + 2, 11].Value = inscripcion.Profesor.Correo;
        worksheet.Cells[i + 2, 12].Value = inscripcion.Profesor.Telefono;
        worksheet.Cells[i + 2, 13].Value = inscripcion.Decano.Nombre;
        worksheet.Cells[i + 2, 14].Value = inscripcion.Carrera.Nombre;
        worksheet.Cells[i + 2, 15].Value = inscripcion.Universidad.Nombre;
        worksheet.Cells[i + 2, 16].Value = inscripcion.EstadoDeInscripcion;
      }

      var stream = new MemoryStream();
      package.SaveAs(stream);

      var content = stream.ToArray();

      return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Inscripciones.xlsx");
    }
  }
}