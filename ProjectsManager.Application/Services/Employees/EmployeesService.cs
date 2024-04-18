using ProjectsManager.Application.DTO;
using ProjectsManager.Domain.Abstractions.Employees;
using ProjectsManager.Domain.Entities.Employees;
using ProjectsManager.Domain.Entities.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ProjectsManager.Application.Services.Employees
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeesRepository;
        private IMapper _mapper;

        public EmployeesService(IEmployeesRepository employeesRepository,IMapper mapper) 
        {
            _employeesRepository = employeesRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<EmployeesDto>>> GetAll()
        {
            Result<List<EmployeesDto>> allEmployeesAndResult = new Result<List<EmployeesDto>>();
            try
            {
                Result<List<EmployeesData>> result = await _employeesRepository.GetAll();
                if (result.Code == ResultCodes.Success)
                {
                    allEmployeesAndResult.Code = ResultCodes.Success;
                    allEmployeesAndResult.Entity = _mapper.Map<List<EmployeesDto>>(result.Entity);
                }
                else
                {
                    allEmployeesAndResult.Code = result.Code;
                    allEmployeesAndResult.Message = result.Message;
                }
            }
            catch (Exception ex)
            {
                allEmployeesAndResult.Code = ResultCodes.Failure;
                allEmployeesAndResult.Message = ex.Message;
            }
            return allEmployeesAndResult;
        }

        public async Task<Result<EmployeesDto>> GetById(int Id)
        {
            Result<EmployeesDto> employeeAndResult = new Result<EmployeesDto>();
            try
            {
                Result<EmployeesData> result = await _employeesRepository.GetById(Id);
                if (result.Code == ResultCodes.Success)
                {
                    employeeAndResult.Code = ResultCodes.Success;
                    employeeAndResult.Entity = _mapper.Map<EmployeesDto>(result.Entity);
                }
                else
                {
                    employeeAndResult.Code = result.Code;
                    employeeAndResult.Message = result.Message;
                }
            }
            catch (Exception ex)
            {
                employeeAndResult.Code = ResultCodes.Failure;
                employeeAndResult.Message = ex.Message;
            }
            return employeeAndResult;
        }

        public async Task<Result> AddEmployee(EmployeesDto employeeDTO)
        {
            EmployeesData employee = _mapper.Map<EmployeesData>(employeeDTO);
            Result resultCreating = await _employeesRepository.Create(employee);
            return resultCreating;
        }

        public async Task<Result> UpdateEmployee(EmployeesDto employeeDTO)
        {
            EmployeesData employee = _mapper.Map<EmployeesData>(employeeDTO);
            Result resultUpdate = await _employeesRepository.Update(employee);
            return resultUpdate;
        }

        public async Task<Result> DeleteEmployee(int Id)
        {
            Result resultDeletion = new Result();
            var employeeDTO = await GetById(Id);
            if (employeeDTO.Code == ResultCodes.Success)
            {
                var employee = _mapper.Map<EmployeesData>(employeeDTO.Entity);
                resultDeletion = await _employeesRepository.Delete(employee);
            }
            else
            {
                resultDeletion.Code = ResultCodes.Failure;
                resultDeletion.Message = employeeDTO.Message;
            }
            return resultDeletion;
        }

    }
}
