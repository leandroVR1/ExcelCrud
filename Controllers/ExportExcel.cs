using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ExcelCrudMVC.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

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
            //Se agrega una nueva hoja de trabajo al paquete con el nombre "Inscripciones".
            var worksheet = package.Workbook.Worksheets.Add("Inscripciones");

            // Encabezados
            string[] headers = {
                "ID Inscripcion", "Nombre Estudiante", "Apellido Estudiante", "Correo Estudiante",
                "Teléfono Estudiante", "Nombre Materia", "Semestre", "Año", "Nombre Profesor",
                "Apellido Profesor", "Correo Profesor", "Teléfono Profesor", "Decano Universidad",
                "Carrera", "Universidad", "Estado de Inscripción"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = headers[i];
                worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet.Cells[1, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // Itera sobre la lista de inscripciones y rellena la hoja de trabajo con datos
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

                for (int j = 1; j <= headers.Length; j++)
                {
                    worksheet.Cells[i + 2, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }
            }

            // Ajusta el tamaño de las columnas automáticamente
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();


            //Guardar y Devolver el Archivo de Excel
            var stream = new MemoryStream();
            package.SaveAs(stream);

            var content = stream.ToArray();

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Inscripciones.xlsx");
        }
    }
}
