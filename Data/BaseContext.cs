using Microsoft.EntityFrameworkCore;
using ExcelCrudMVC.Models;

namespace ExcelCrudMVC.Data{
public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
  {
  }

  public DbSet<Estudiante> Estudiantes { get; set; }
  public DbSet<Materia> Materias { get; set; }
  public DbSet<Profesor> Profesores { get; set; }
  public DbSet<Decano> Decanos { get; set; }
  public DbSet<Universidad> Universidades { get; set; }
  public DbSet<Carrera> Carreras { get; set; }
  public DbSet<Inscripcion> Inscripciones { get; set; }
}
}