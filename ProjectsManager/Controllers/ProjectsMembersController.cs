using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectsManager.Application.DTO;
using ProjectsManager.Application.Services.Employees;
using ProjectsManager.Application.Services.Projects;
using ProjectsManager.Application.Services.ProjectsMembers;
using ProjectsManager.Domain.Entities.Frameworks;
using ProjectsManager.ViewModels;

namespace ProjectsManager.Controllers
{
    public class ProjectsMembersController : Controller
    {
        private readonly IProjectsMembersService _projectsMembersService;
        private readonly IProjectsService _projectsService;
        private readonly IEmployeesService _employeesService;

        public ProjectsMembersController(IProjectsMembersService projectsMembersService,IProjectsService projectsService,IEmployeesService employeesService)
        {
            _projectsMembersService= projectsMembersService;
            _projectsService= projectsService;
            _employeesService= employeesService;
        }

        [HttpGet]
        public async Task<IActionResult> AllProjectsMembers(int? projectId)
        {
            if (projectId == null)
            {
                var resultService = await _projectsMembersService.GetAllProjectsMembers();
                if (resultService.Code == ResultCodes.Failure)
                {
                    ErrorModel errorModel = new ErrorModel();
                    errorModel.ErrorTitle = "Ошибка вывода сотрудников и проектов!";
                    errorModel.ErrorDescription = $"Произошла ошибка при выводе сотрудников и проектов. {resultService.Message}";
                    return View("~/Views/Error/Error.cshtml", errorModel);
                }
                else
                    return View(resultService.Entity);
            }
            else
            {
                var resultService = await _projectsMembersService.GetProjectMemberByProjectId((int)projectId);
                if (resultService.Code == ResultCodes.Failure)
                {
                    ErrorModel errorModel = new ErrorModel();
                    errorModel.ErrorTitle = "Ошибка вывода сотрудников и проектов!";
                    errorModel.ErrorDescription = $"Произошла ошибка при выводе сотрудников и проектов. {resultService.Message}";
                    return View("~/Views/Error/Error.cshtml", errorModel);
                }
                else
                {
                    ViewBag.ProjectId=projectId;
                    return View(resultService.Entity);
                }
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Add(int projectId)
        {
            ViewBag.ProjectName = (await _projectsService.GetById(projectId)).Entity.Name;
            // Список сотрудников для добавления в текущий проект
            ViewData["EmployeeList"] = new SelectList((await _employeesService.GetAll()).Entity, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] ProjectsMembersDto projectMemberDTO)
        {
            if (ModelState.IsValid)
            {
                // Получаем результат существующих участников проекта
                var result = await _projectsMembersService.GetProjectMemberByProjectId(projectMemberDTO.ProjectId);

                // Проверяем, успешен ли результат и содержит ли он список
                if (result.Code == ResultCodes.Success)
                {
                    var existingMembers = result.Entity; // Распаковываем список участников

                    // Проверяем, есть ли уже сотрудник проекта в текущем проекте
                    bool hasProjectManager = existingMembers.Any(member => member.IsProjectManager);

                    if (hasProjectManager)
                    {
                        // Если уже есть сотрудник проекта, верните ошибку
                        ModelState.AddModelError("", "Невозможно добавить нового участника, так как в проекте уже есть этот сотудник.");
                        // Обновляем ViewBag и ViewData, если это требуется
                        ViewBag.ProjectId = projectMemberDTO.ProjectId;
                        ViewBag.ProjectName = projectMemberDTO.ProjectName;
                        ViewData["EmployeeList"] = new SelectList((await _employeesService.GetAll()).Entity, "Id", "Name");
                        return View(projectMemberDTO);
                    }
                }

                // Если менеджера проекта нет, добавляем нового участника
                var resultService = await _projectsMembersService.AddProjectMember(projectMemberDTO);
                if (resultService.Code == ResultCodes.Success)
                {
                    return Redirect(nameof(AllProjectsMembers));
                }
                else
                {
                    ErrorModel errorModel = new ErrorModel();
                    errorModel.ErrorTitle = "Ошибка при добавлении участников в проекты";
                    errorModel.ErrorDescription = $"Произошла ошибка при добавлении участников в проекты. {resultService.Message}";
                    return View("~/Views/Error/Error.cshtml", errorModel);
                }
            }

            ViewBag.ProjectId = projectMemberDTO.ProjectId;
            ViewBag.ProjectName = projectMemberDTO.ProjectName;
            // Список сотрудников для добавления в текущий проект
            ViewData["EmployeeList"] = new SelectList((await _employeesService.GetAll()).Entity, "Id", "Name");
            return View(projectMemberDTO);
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var resultService = await _projectsMembersService.GetProjectMemberById(Id);
            if (resultService.Code == ResultCodes.Failure)
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка при обновлении данных участников в проекте";
                errorModel.ErrorDescription = $"Произошла ошибка при обновлении данных участников в проекте. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
            else if (resultService.Code == ResultCodes.NotFound)
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка при обновлении данных участников в проекте";
                errorModel.ErrorDescription = $"Произошла ошибка при обновлении данных участников в проекте. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
            else
            {
                ViewBag.ProjectId = resultService.Entity.ProjectId;
                ViewBag.ProjectName = (await _projectsService.GetById(resultService.Entity.ProjectId)).Entity.Name;
                // Список сотрудников для добавления в текущий проект
                ViewData["EmployeeList"] = new SelectList((await _employeesService.GetAll()).Entity.Where(x=>x.Id==resultService.Entity.EmployeesId), "Id", "Name");
                return View(resultService.Entity);
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed([FromForm] ProjectsMembersDto projectMemberDTO)
        {
            if (ModelState.IsValid)
            {
                var resultService = await _projectsMembersService.UpdateProjectMember(projectMemberDTO);
                if (resultService.Code == ResultCodes.Success)
                    return RedirectToAction(nameof(AllProjectsMembers));
                else
                {
                    ErrorModel errorModel = new ErrorModel();
                    errorModel.ErrorTitle = "Ошибка при обновлении данных участников в проекте";
                    errorModel.ErrorDescription = $"Произошла ошибка при обновлении данных участников в проекте. {resultService.Message}";
                    return View("~/Views/Error/Error.cshtml", errorModel);
                }
            }
            ViewBag.ProjectId = projectMemberDTO.ProjectId;
            ViewBag.ProjectName = projectMemberDTO.ProjectName;
            // Список сотрудников для добавления в текущий проект
            ViewData["EmployeeList"] = new SelectList((await _employeesService.GetAll()).Entity, "Id", "Name");
            return View(projectMemberDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var resultService = await _projectsMembersService.GetProjectMemberById(Id);
            if (resultService.Code == ResultCodes.Failure)
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка при удалении участника из проекта";
                errorModel.ErrorDescription = $"Произошла ошибка при удалении участника из проекта. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
            else if (resultService.Code == ResultCodes.NotFound)
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка при удалении участника из проекта";
                errorModel.ErrorDescription = $"Произошла ошибка при удалении участника из проекта. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
            else
                return View(resultService.Entity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var resultService = await _projectsMembersService.DeleteProjectMember(Id);
            if (resultService.Code == ResultCodes.Success)
                return RedirectToAction(nameof(AllProjectsMembers));
            else
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка при удалении участника из проекта";
                errorModel.ErrorDescription = $"Произошла ошибка при удалении участника из проекта. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
        }
    }
}
