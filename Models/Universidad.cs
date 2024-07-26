namespace ExcelCrudMVC.Models{
  public class Universidad
  {
    public int UniversidadID { get; set; }
    public string Nombre { get; set; }

    public ICollection<Inscripcion> Inscripciones { get; set; }
  }

}