namespace ExcelCrudMVC.Models{
  public class Materia
  {
    public int MateriaID { get; set; }
    public string Nombre { get; set; }

    public ICollection<Inscripcion> Inscripciones { get; set; }
  }

}