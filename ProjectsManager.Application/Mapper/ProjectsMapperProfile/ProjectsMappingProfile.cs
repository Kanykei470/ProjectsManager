using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProjectsManager.Application.DTO;
using ProjectsManager.Domain.Entities.Projects;

namespace ProjectsManager.Application.Mapper.ProjectsMapperProfile
{
    public class ProjectsMappingProfile : Profile
    {
        public ProjectsMappingProfile()
        {
            CreateMap<ProjectsData, ProjectsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.ExecutorName, opt => opt.MapFrom(src => src.ExecutorName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority));

            CreateMap<ProjectsDto, ProjectsData>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.ExecutorName, opt => opt.MapFrom(src => src.ExecutorName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.ProjectMembers, opt => opt.Ignore()); // Игнорируем ProjectMembers в обратном направлении
        }
    }
}
