namespace ExcelCrudMVC.Models{
  public class Profesor
  {
    public int ProfesorID { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Correo { get; set; }
    public string Telefono { get; set; }

    public ICollection<Inscripcion> Inscripciones { get; set; }
  }

}