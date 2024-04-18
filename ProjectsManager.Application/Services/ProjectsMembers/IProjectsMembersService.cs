using ProjectsManager.Application.DTO;
using ProjectsManager.Domain.Entities.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManager.Application.Services.ProjectsMembers
{
    public interface IProjectsMembersService
    {
        Task<Result> AddProjectMember(ProjectsMembersDto projectMemberDTO);
        Task<Result> DeleteProjectMember(int Id);
        Task<Result> UpdateProjectMember(ProjectsMembersDto projectMemberDTO);
        Task<Result<List<ProjectsMembersDto>>> GetAllProjectsMembers();
        Task<Result<ProjectsMembersDto>> GetProjectMemberById(int Id);
        Task<Result<List<ProjectsMembersDto>>> GetProjectMemberByProjectId(int Id);
       
    }
}
