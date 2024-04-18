using ProjectsManager.Domain.Entities.Frameworks;
using ProjectsManager.Domain.Entities.ProjectMembers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManager.Domain.Abstractions.ProjectMembers
{
    public interface IProjectsMembersRepository : IRepository<ProjectMembersData>
    {
        Task<Result<List<ProjectMembersData>>> GetAllDataByProjectId(int projectId);
    }
}
