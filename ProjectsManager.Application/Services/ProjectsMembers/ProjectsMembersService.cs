using AutoMapper;
using ProjectsManager.Application.DTO;
using ProjectsManager.Domain.Abstractions.ProjectMembers;
using ProjectsManager.Domain.Entities.Frameworks;
using ProjectsManager.Domain.Entities.ProjectMembers;
using ProjectsManager.Domain.Entities.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManager.Application.Services.ProjectsMembers
{
    public class ProjectsMembersService : IProjectsMembersService
    {
        private readonly IProjectsMembersRepository _projectsMembersRepository;
        private IMapper _mapper;

        public ProjectsMembersService(IProjectsMembersRepository projectsMembersRepository, IMapper mapper)
        {
            _projectsMembersRepository = projectsMembersRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<ProjectsMembersDto>>> GetAllProjectsMembers()
        {
            Result<List<ProjectsMembersDto>> allProjectsMembersAndResult = new Result<List<ProjectsMembersDto>>();
            try
            {
                Result<List<ProjectMembersData>> result = await _projectsMembersRepository.GetAll();
                if (result.Code == ResultCodes.Success)
                {
                    allProjectsMembersAndResult.Code = ResultCodes.Success;
                    allProjectsMembersAndResult.Entity = _mapper.Map<List<ProjectsMembersDto>>(result.Entity);
                }
                else
                {
                    allProjectsMembersAndResult.Code = result.Code;
                    allProjectsMembersAndResult.Message = result.Message;
                }
            }
            catch (Exception ex)
            {
                allProjectsMembersAndResult.Code = ResultCodes.Failure;
                allProjectsMembersAndResult.Message = ex.Message;
            }
            return allProjectsMembersAndResult;
        }

        public async Task<Result<ProjectsMembersDto>> GetProjectMemberById(int Id)
        {
            Result<ProjectsMembersDto> projectMemberAndResult = new Result<ProjectsMembersDto>();
            try
            {
                Result<ProjectMembersData> result = await _projectsMembersRepository.GetById(Id);
                if (result.Code == ResultCodes.Success)
                {
                    projectMemberAndResult.Code = ResultCodes.Success;
                    projectMemberAndResult.Entity =  _mapper.Map<ProjectsMembersDto>(result.Entity);
                }
                else
                {
                    projectMemberAndResult.Code = result.Code;
                    projectMemberAndResult.Message = result.Message;
                }
            }
            catch (Exception ex)
            {
                projectMemberAndResult.Code = ResultCodes.Failure;
                projectMemberAndResult.Message = ex.Message;
            }
            return projectMemberAndResult;
        }

        public async Task<Result> AddProjectMember(ProjectsMembersDto projectMemberDTO)
        {
            ProjectMembersData projectMember = _mapper.Map<ProjectMembersData>(projectMemberDTO);
            Result resultCreating = await _projectsMembersRepository.Create(projectMember);
            return resultCreating;
        }

        public async Task<Result> UpdateProjectMember(ProjectsMembersDto projectMemberDTO)
        {
            ProjectMembersData projectMember = _mapper.Map<ProjectMembersData>(projectMemberDTO);
            Result resultUpdate = await _projectsMembersRepository.Update(projectMember);
            return resultUpdate;
        }

        public async Task<Result> DeleteProjectMember(int Id)
        {
            Result resultDeletion = new Result();
            var projectMemberDTO = await GetProjectMemberById(Id);
            if (projectMemberDTO.Code == ResultCodes.Success)
            {
                var projectMember = _mapper.Map<ProjectMembersData>(projectMemberDTO.Entity);
                resultDeletion = await _projectsMembersRepository.Delete(projectMember);
            }
            else
            {
                resultDeletion.Code = ResultCodes.Failure;
                resultDeletion.Message = projectMemberDTO.Message;
            }
            return resultDeletion;
        }

        public async Task<Result<List<ProjectsMembersDto>>> GetProjectMemberByProjectId(int Id)
        {
            Result<List<ProjectsMembersDto>> allProjectsMembersAndResult = new Result<List<ProjectsMembersDto>>();
            try
            {
                Result<List<ProjectMembersData>> result = await _projectsMembersRepository.GetAllDataByProjectId(Id);
                if (result.Code == ResultCodes.Success)
                {
                    allProjectsMembersAndResult.Code = ResultCodes.Success;
                    allProjectsMembersAndResult.Entity = _mapper.Map<List<ProjectsMembersDto>>(result.Entity);
                }
                else if (result.Code == ResultCodes.NotFound)
                {
                    allProjectsMembersAndResult.Code = result.Code;
                    allProjectsMembersAndResult.Message = result.Message;
                    allProjectsMembersAndResult.Entity = _mapper.Map<List<ProjectsMembersDto>>(result.Entity);
                }
                else
                {
                    allProjectsMembersAndResult.Code = result.Code;
                    allProjectsMembersAndResult.Message = result.Message;
                }
            }
            catch (Exception ex)
            {
                allProjectsMembersAndResult.Code = ResultCodes.Failure;
                allProjectsMembersAndResult.Message = ex.Message;
            }
            return allProjectsMembersAndResult;
        }
    }
}
