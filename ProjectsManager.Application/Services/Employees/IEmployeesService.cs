using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsManager.Application.DTO;
using ProjectsManager.Domain.Entities.Frameworks;

namespace ProjectsManager.Application.Services.Employees
{
    public interface IEmployeesService
    {
        Task<Result> AddEmployee(EmployeesDto employeesDto);

        Task<Result> UpdateEmployee(EmployeesDto employeesDto);

        Task<Result> DeleteEmployee(int id);

        Task<Result<List<EmployeesDto>>> GetAll();
        Task<Result<EmployeesDto>> GetById(int id);
    }
}
