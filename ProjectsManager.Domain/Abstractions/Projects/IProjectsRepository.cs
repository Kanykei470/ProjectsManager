using ProjectsManager.Domain.Entities.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManager.Domain.Abstractions.Projects
{
    public interface IProjectsRepository : IRepository<ProjectsData>
    {

    }
}
