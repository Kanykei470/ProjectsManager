using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Infrastructure.Repositories.EmployeesRepo;
using ProjectManager.Infrastructure.Repositories.ProjectMembersRepo;
using ProjectManager.Infrastructure.Repositories.ProjectsRepo;
using ProjectsManager.Domain.Abstractions.Employees;
using ProjectsManager.Domain.Abstractions.ProjectMembers;
using ProjectsManager.Domain.Abstractions.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Infrastructure.Extensions
{
    public static class Injection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IProjectsMembersRepository, ProjectMembersRepository>();
            return services;
        }
    }
}
