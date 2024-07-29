// Profiles/MappingProfile.cs
using AutoMapper;
using ExcelCrudMVC.Models;
using ExcelCrudMVC.ViewModels;

namespace ExcelCrudMVC.Profiles{
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Estudiante, EstudianteViewModel>().ReverseMap();
        //CreateMap<Inscripcion, InscripcionViewModel>().ReverseMap();
          CreateMap<Inscripcion, InscripcionViewModel>()
            .ForMember(dest => dest.EstudianteNombre, opt => opt.MapFrom(src => src.Estudiante.Nombre + " " + src.Estudiante.Apellido))
            .ForMember(dest => dest.MateriaNombre, opt => opt.MapFrom(src => src.Materia.Nombre))
            .ForMember(dest => dest.ProfesorNombre, opt => opt.MapFrom(src => src.Profesor.Nombre + " " + src.Profesor.Apellido))
            .ForMember(dest => dest.DecanoNombre, opt => opt.MapFrom(src => src.Decano.Nombre))
            .ForMember(dest => dest.UniversidadNombre, opt => opt.MapFrom(src => src.Universidad.Nombre))
            .ForMember(dest => dest.CarreraNombre, opt => opt.MapFrom(src => src.Carrera.Nombre))
            .ReverseMap();
        
    }
}
}
