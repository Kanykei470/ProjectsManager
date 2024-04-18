using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Infrastructure.DataContext;
using ProjectsManager.Domain.Abstractions.Projects;
using ProjectsManager.Domain.Entities.Frameworks;
using ProjectsManager.Domain.Entities.Projects;

namespace ProjectManager.Infrastructure.Repositories.ProjectsRepo
{
    internal class ProjectsRepository : IProjectsRepository
    {
        private readonly ApplicationDBContext _dbcontext;
        public ProjectsRepository(ApplicationDBContext applicationDbContext)
        {
            _dbcontext = applicationDbContext;
        }

        public async Task<Result<List<ProjectsData>>> GetAll()
        {
            Result<List<ProjectsData>> result = new Result<List<ProjectsData>>();
            try
            {
                result.Code = ResultCodes.Success;
                result.Entity = await _dbcontext.Projects.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                result.Code = ResultCodes.Failure;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<Result<ProjectsData>> GetById(int Id)
        {
            Result<ProjectsData> result = new Result<ProjectsData>();
            try
            {
                var employeeById = await _dbcontext.Projects.AsNoTracking().Where(x => x.Id == Id).FirstOrDefaultAsync();
                if (employeeById == null)
                {
                    result.Code = ResultCodes.NotFound;
                    result.Message = $"Нет проекта по Id: {Id}";
                }
                else
                {
                    result.Code = ResultCodes.Success;
                    result.Entity = employeeById;
                }
            }
            catch (Exception ex)
            {
                result.Code = ResultCodes.Failure;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<Result> Create(ProjectsData projects)
        {
            Result result = new Result();
            try
            {
                _dbcontext.Projects.Add(projects);
                await _dbcontext.SaveChangesAsync();
                result.Code = ResultCodes.Success;
            }
            catch (Exception ex)
            {
                result.Code = ResultCodes.Failure;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<Result> Update(ProjectsData projects)
        {
            Result result = new Result();
            try
            {
                _dbcontext.Projects.Update(projects);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Code = ResultCodes.Failure;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<Result> Delete(ProjectsData projects)
        {
            Result result = new Result();
            try
            {
                _dbcontext.Projects.Remove(projects);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Code = ResultCodes.Failure;
                result.Message = ex.Message;
            }
            return result;
        }

    }
}
