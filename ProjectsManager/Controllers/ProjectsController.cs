using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectsManager.Application.DTO;
using ProjectsManager.Application.Services.Projects;
using ProjectsManager.Domain.Entities.Frameworks;
using ProjectsManager.ViewModels;

namespace ProjectsManager.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService) {
            _projectsService = projectsService;
        }

        [HttpGet]
        public async Task<IActionResult> AllProjects()
        {
            var resultService = await _projectsService.GetAll();
            if (resultService.Code == ResultCodes.Failure)
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка вывода проектов!";
                errorModel.ErrorDescription = $"Произошла ошибка при выводе проектов. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
            else
            {
                ViewData["Priority"] = new SelectList(resultService.Entity
               .Select(x => x.Priority).Distinct()
               .ToList());

                return View(resultService.Entity);
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] ProjectsDto projectDTO)
        {
            if (ModelState.IsValid)
            {
                var resultService = await _projectsService.AddProjects(projectDTO);
                if (resultService.Code == ResultCodes.Success)
                    return Redirect(nameof(AllProjects));
                else
                {
                    ErrorModel errorModel = new ErrorModel();
                    errorModel.ErrorTitle = "Ошибка при добавлении проекта";
                    errorModel.ErrorDescription = $"Произошла ошибка при добавлении проекта. {resultService.Message}";
                    return View("~/Views/Error/Error.cshtml", errorModel);
                }
            }
            return View(projectDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var resultService = await _projectsService.GetById(Id);
            if (resultService.Code == ResultCodes.Failure)
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка при обновлении данных проекта";
                errorModel.ErrorDescription = $"Произошла ошибка при обновлении данных проекта. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
            else if (resultService.Code == ResultCodes.NotFound)
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка при обновлении данных проекта";
                errorModel.ErrorDescription = $"Произошла ошибка при обновлении данных проекта. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
            else
                return View(resultService.Entity);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed([FromForm] ProjectsDto projectDTO)
        {
            if (ModelState.IsValid)
            {
                var resultService = await _projectsService.UpdateProjects(projectDTO);
                if (resultService.Code == ResultCodes.Success)
                    return RedirectToAction(nameof(AllProjects));
                else
                {
                    ErrorModel errorModel = new ErrorModel();
                    errorModel.ErrorTitle = "Ошибка при обновлении данных проекта";
                    errorModel.ErrorDescription = $"Произошла ошибка при обновлении данных проекта. {resultService.Message}";
                    return View("~/Views/Error/Error.cshtml", errorModel);
                }
            }
            return View(projectDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var resultService = await _projectsService.GetById(Id);
            if (resultService.Code == ResultCodes.Failure)
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка при удалении проекта";
                errorModel.ErrorDescription = $"Произошла ошибка при удалении проекта. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
            else if (resultService.Code == ResultCodes.NotFound)
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка при удалении проекта";
                errorModel.ErrorDescription = $"Произошла ошибка при удалении проекта. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
            else
                return View(resultService.Entity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var resultService = await _projectsService.DeleteProjects(Id);
            if (resultService.Code == ResultCodes.Success)
                return RedirectToAction(nameof(AllProjects));
            else
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка при удалении проекта";
                errorModel.ErrorDescription = $"Произошла ошибка при удалении проекта. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
        }

    }
}
