using AutoMapper;
using ProjectsManager.Application.DTO;
using ProjectsManager.Domain.Abstractions.Projects;
using ProjectsManager.Domain.Entities.Employees;
using ProjectsManager.Domain.Entities.Frameworks;
using ProjectsManager.Domain.Entities.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManager.Application.Services.Projects
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;
        private IMapper _mapper;

        public ProjectsService(IProjectsRepository projectsRepository, IMapper mapper)
        {
            _projectsRepository = projectsRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<ProjectsDto>>> GetAll()
        {
            Result<List<ProjectsDto>> allProjectsAndResult = new Result<List<ProjectsDto>>();
            try
            {
                Result<List<ProjectsData>> result = await _projectsRepository.GetAll();
                if (result.Code == ResultCodes.Success)
                {
                    allProjectsAndResult.Code = ResultCodes.Success;
                    allProjectsAndResult.Entity = _mapper.Map<List<ProjectsDto>>(result.Entity);
                }
                else
                {
                    allProjectsAndResult.Code = result.Code;
                    allProjectsAndResult.Message = result.Message;
                }
            }
            catch (Exception ex)
            {
                allProjectsAndResult.Code = ResultCodes.Failure;
                allProjectsAndResult.Message = ex.Message;
            }
            return allProjectsAndResult;
        }

        public async Task<Result<ProjectsDto>> GetById(int Id)
        {
            Result<ProjectsDto> projectAndResult = new Result<ProjectsDto>();
            try
            {
                Result<ProjectsData> result = await _projectsRepository.GetById(Id);
                if (result.Code == ResultCodes.Success)
                {
                    projectAndResult.Code = ResultCodes.Success;
                    projectAndResult.Entity = _mapper.Map<ProjectsDto>(result.Entity);
                }
                else
                {
                    projectAndResult.Code = result.Code;
                    projectAndResult.Message = result.Message;
                }
            }
            catch (Exception ex)
            {
                projectAndResult.Code = ResultCodes.Failure;
                projectAndResult.Message = ex.Message;
            }
            return projectAndResult;
        }

        public async Task<Result> AddProjects(ProjectsDto projectDTO)
        {
            ProjectsData project = _mapper.Map<ProjectsData>(projectDTO);
            Result resultCreating = await _projectsRepository.Create(project);
            return resultCreating;
        }

        public async Task<Result> UpdateProjects(ProjectsDto projectDTO)
        {
            ProjectsData project = _mapper.Map<ProjectsData>(projectDTO);
            Result resultUpdate = await _projectsRepository.Update(project);
            return resultUpdate;
        }

        public async Task<Result> DeleteProjects(int Id)
        {
            Result resultDeletion = new Result();
            var projectDTO = await GetById(Id);
            if (projectDTO.Code == ResultCodes.Success)
            {
                var project = _mapper.Map<ProjectsData>(projectDTO.Entity);
                resultDeletion = await _projectsRepository.Delete(project);
            }
            else
            {
                resultDeletion.Code = ResultCodes.Failure;
                resultDeletion.Message = projectDTO.Message;
            }
            return resultDeletion;
        }
    }
}
