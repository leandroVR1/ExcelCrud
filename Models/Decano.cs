namespace ExcelCrudMVC.Models{
  public class Decano
  {
    public int DecanoID { get; set; }
    public string Nombre { get; set; }

    public ICollection<Inscripcion> Inscripciones { get; set; }
  }

}