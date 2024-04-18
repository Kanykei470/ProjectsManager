using Microsoft.Extensions.DependencyInjection;
using ProjectsManager.Application.Mapper.EmployeesMapperProfile;
using ProjectsManager.Application.Mapper.ProjectsMapperProfile;
using ProjectsManager.Application.Mapper.ProjectsMembersMapperProfile;
using ProjectsManager.Application.Services.Employees;
using ProjectsManager.Application.Services.Projects;
using ProjectsManager.Application.Services.ProjectsMembers;

namespace ProjectsManager.Application.Extensions
{
    public static class Injection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeesService, EmployeesService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<IProjectsMembersService, ProjectsMembersService>();
            services.AddAutoMapper(typeof(EmployeesMapperProfile));
            services.AddAutoMapper(typeof(ProjectsMappingProfile));
            services.AddAutoMapper(typeof(ProjectsMembersMappingProfile));
            return services;
        }
    }
}
