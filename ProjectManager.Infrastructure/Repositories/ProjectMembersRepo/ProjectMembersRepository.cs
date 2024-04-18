using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Infrastructure.DataContext;
using ProjectsManager.Domain.Abstractions;
using ProjectsManager.Domain.Abstractions.ProjectMembers;
using ProjectsManager.Domain.Entities.Frameworks;
using ProjectsManager.Domain.Entities.ProjectMembers;

namespace ProjectManager.Infrastructure.Repositories.ProjectMembersRepo
{
    internal class ProjectMembersRepository : IProjectsMembersRepository
    {
        private readonly ApplicationDBContext _dbcontext;

        public ProjectMembersRepository(ApplicationDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Result<List<ProjectMembersData>>> GetAll()
        {
            Result<List<ProjectMembersData>> result = new Result<List<ProjectMembersData>>();
            try
            {
                result.Code = ResultCodes.Success;
                result.Entity = await _dbcontext.ProjectsMembers
                    .Include(emp => emp.Employee)
                    .Include(pr => pr.Project)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                result.Code = ResultCodes.Failure;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<Result<ProjectMembersData>> GetById(int Id)
        {
            Result<ProjectMembersData> result = new Result<ProjectMembersData>();
            try
            {
                var projectMemberById = await _dbcontext.ProjectsMembers
                    .Include(emp => emp.Employee)
                    .Include(pr => pr.Project)
                    .AsNoTracking()
                    .Where(x => x.Id == Id).FirstOrDefaultAsync();
                if (projectMemberById == null)
                {
                    result.Code = ResultCodes.NotFound;
                    result.Message = $"Нет проекта по Id: {Id}";
                }
                else
                {
                    result.Code = ResultCodes.Success;
                    result.Entity = projectMemberById;
                }
            }
            catch (Exception ex)
            {
                result.Code = ResultCodes.Failure;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<Result> Create(ProjectMembersData projectMemberData)
        {
            Result result = new Result();
            try
            {
                _dbcontext.ProjectsMembers.Add(projectMemberData);
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

        public async Task<Result> Update(ProjectMembersData projectMemberData)
        {
            Result result = new Result();
            try
            {
                _dbcontext.ProjectsMembers.Update(projectMemberData);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Code = ResultCodes.Failure;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<Result> Delete(ProjectMembersData projectMemberData)
        {
            Result result = new Result();
            try
            {
                _dbcontext.ProjectsMembers.Remove(projectMemberData);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Code = ResultCodes.Failure;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<Result<List<ProjectMembersData>>> GetAllDataByProjectId(int projectId)
        {
            Result<List<ProjectMembersData>> result = new Result<List<ProjectMembersData>>();
            try
            {
                var projectsMembersByProjectId = await _dbcontext.ProjectsMembers
                      .Include(emp => emp.Employee)
                      .Include(pr => pr.Project)
                      .AsNoTracking()
                      .Where(x => x.ProjectId == projectId).ToListAsync();

                if (projectsMembersByProjectId.Count == 0)
                {
                    result.Code = ResultCodes.NotFound;
                    result.Message = $"Нет проекта по Id: {projectId}";
                    result.Entity = projectsMembersByProjectId;
                }
                else
                {
                    result.Code = ResultCodes.Success;
                    result.Entity = projectsMembersByProjectId;
                }
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
