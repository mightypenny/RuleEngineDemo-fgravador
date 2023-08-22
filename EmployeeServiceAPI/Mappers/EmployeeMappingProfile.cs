using AutoMapper;
using EmployeeService.Core.Models;
using EmployeeService.Models;
using MongoDB.Bson;

namespace EmployeeService.Mappers;

public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile()
    {
        // DTO to Domain
        CreateMap<EmployeeDto, Employee>();
        
        // Domain to DTO
        CreateMap<Employee, EmployeeDto>();
        CreateMap<Employee, EmployeeExtDto>()
            .ForMember(
                dest => dest.FullName, 
                opt => opt.MapFrom(src => $"{src.LastName}, {src.FirstName}"))
            .ForMember(
                dest => dest.BonusDifferentName,
                opt => opt.MapFrom(src => src.Bonus()));
        
        // Domain to Persistence MongoDB
        CreateMap<Repositories.MongoDB.Employee, Employee>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id.ToString()));
        
        CreateMap<Employee, Repositories.MongoDB.Employee>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => ObjectId.GenerateNewId()));
        
        // Domain to Persistence MongoDB
        CreateMap<Repositories.Postgres.Employee, Employee>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id.ToString()));
        
        CreateMap<Employee, Repositories.Postgres.Employee>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => ObjectId.GenerateNewId()));
    }
}