using System.Collections.Generic;
using ExcelCrudMVC.Models;

namespace ExcelCrudMVC.ViewModels
{
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

        public List<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
        public List<Materia> Materias { get; set; } = new List<Materia>();
        public List<Profesor> Profesores { get; set; } = new List<Profesor>();
        public List<Decano> Decanos { get; set; } = new List<Decano>();
        public List<Universidad> Universidades { get; set; } = new List<Universidad>();
        public List<Carrera> Carreras { get; set; } = new List<Carrera>();
    }
}
