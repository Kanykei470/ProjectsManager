using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Infrastructure.DataContext;
using ProjectsManager.Domain.Abstractions.Employees;
using ProjectsManager.Domain.Entities.Employees;
using ProjectsManager.Domain.Entities.Frameworks;

namespace ProjectManager.Infrastructure.Repositories.EmployeesRepo
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly ApplicationDBContext _dbcontext;
        public EmployeesRepository(ApplicationDBContext applicationDbContext)
        {
            _dbcontext = applicationDbContext;
        }

        public async Task<Result<List<EmployeesData>>> GetAll()
        {
            Result<List<EmployeesData>> result = new Result<List<EmployeesData>>();
            try
            {
                result.Code = ResultCodes.Success;
                result.Entity = await _dbcontext.Employees.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                result.Code = ResultCodes.Failure;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<Result<EmployeesData>> GetById(int Id)
        {
            Result<EmployeesData> result = new Result<EmployeesData>();
            try
            {
                var employeeById = await _dbcontext.Employees.AsNoTracking().Where(x => x.Id == Id).FirstOrDefaultAsync();
                if (employeeById == null)
                {
                    result.Code = ResultCodes.NotFound;
                    result.Message = $"Нет сотрудника по Id: {Id}";
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

        public async Task<Result> Create(EmployeesData employee)
        {
            Result result = new Result();
            try
            {
                _dbcontext.Employees.Add(employee);
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

        public async Task<Result> Update(EmployeesData employee)
        {
            Result result = new Result();
            try
            {
                _dbcontext.Employees.Update(employee);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Code = ResultCodes.Failure;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<Result> Delete(EmployeesData employee)
        {
            Result result = new Result();
            try
            {
                _dbcontext.Employees.Remove(employee);
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
