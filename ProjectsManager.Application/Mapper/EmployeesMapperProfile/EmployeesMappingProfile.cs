using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProjectsManager.Application.DTO;
using ProjectsManager.Domain.Entities.Employees;

namespace ProjectsManager.Application.Mapper.EmployeesMapperProfile
{
    public class EmployeesMapperProfile : Profile
    {
        public EmployeesMapperProfile()
        {
            CreateMap<EmployeesData, EmployeesDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // Прямое маппинг Id из src.Id в dest.Id
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<EmployeesDto, EmployeesData>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.ProjectMembers, opt => opt.Ignore());
        }
    }
}
