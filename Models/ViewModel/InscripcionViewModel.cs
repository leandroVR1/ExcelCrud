// ViewModels/InscripcionViewModel.cs
namespace  ExcelCrudMVC.ViewModels{
public class InscripcionViewModel
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
}
}