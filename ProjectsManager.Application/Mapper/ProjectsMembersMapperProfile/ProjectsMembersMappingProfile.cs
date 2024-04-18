using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProjectsManager.Application.DTO;
using ProjectsManager.Domain.Entities.ProjectMembers;
using ProjectsManager.Domain.Entities.Projects;

namespace ProjectsManager.Application.Mapper.ProjectsMembersMapperProfile
{
    public class ProjectsMembersMappingProfile : Profile
    {
        public ProjectsMembersMappingProfile()
        {
            CreateMap<ProjectMembersData, ProjectsMembersDto>()
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project != null ? src.Project.Name : null))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.Name : null));

            CreateMap<ProjectsMembersDto, ProjectMembersData>()
            .ForMember(dest => dest.Project, opt => opt.Ignore())
            .ForMember(dest => dest.Employee, opt => opt.Ignore());
        }
    }
}
