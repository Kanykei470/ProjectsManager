using ProjectsManager.Application.DTO;
using ProjectsManager.Domain.Entities.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManager.Application.Services.Projects
{
    public interface IProjectsService
    {
        Task<Result> AddProjects(ProjectsDto projectsDto);

        Task<Result> UpdateProjects(ProjectsDto projectsDto);

        Task<Result> DeleteProjects(int id);

        Task<Result<List<ProjectsDto>>> GetAll();
        Task<Result<ProjectsDto>> GetById(int id);
    }
}
