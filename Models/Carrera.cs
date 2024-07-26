namespace ExcelCrudMVC.Models{
  public class Carrera
  {
    public int CarreraID { get; set; }
    public string Nombre { get; set; }

    public ICollection<Inscripcion> Inscripciones { get; set; }
  }

}