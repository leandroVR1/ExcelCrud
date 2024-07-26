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
    }
}
}
