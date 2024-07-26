using System.ComponentModel.DataAnnotations;

namespace  ExcelCrudMVC.ViewModels{

public class EstudianteViewModel
{
    public int EstudianteID { get; set; }

    [Required]
    [StringLength(50)]
    public string Nombre { get; set; }

    [Required]
    [StringLength(50)]
    public string Apellido { get; set; }

    [Required]
    [EmailAddress]
    public string Correo { get; set; }

    [Required]
    [Phone]
    public string Telefono { get; set; }
}
}