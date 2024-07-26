namespace ExcelCrudMVC.Models{
  public class Inscripcion
  {
    public int InscripcionID { get; set; }
    public int EstudianteID { get; set; }
    public int MateriaID { get; set; }
    public int ProfesorID { get; set; }
    public int DecanoID { get; set; }
    public int UniversidadID { get; set; }
    public int CarreraID { get; set; }
    public int Semestre { get; set; }
    public int Año { get; set; }
    public string EstadoDeInscripcion { get; set; }

    public Estudiante Estudiante { get; set; }
    public Materia Materia { get; set; }
    public Profesor Profesor { get; set; }
    public Decano Decano { get; set; }
    public Universidad Universidad { get; set; }
    public Carrera Carrera { get; set; }
  }

}