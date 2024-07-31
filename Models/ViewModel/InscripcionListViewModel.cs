using System;

namespace ExcelCrudMVC.ViewModels
{
    public class InscripcionListViewModel
    {
        public int InscripcionID { get; set; }
        public string EstudianteNombre { get; set; }
        public string MateriaNombre { get; set; }
        public string ProfesorNombre { get; set; }
        public string DecanoNombre { get; set; }
        public string UniversidadNombre { get; set; }
        public string CarreraNombre { get; set; }
        public int Semestre { get; set; }
        public int Año { get; set; }
        public string EstadoDeInscripcion { get; set; }
    }
}
