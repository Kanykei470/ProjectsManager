using Microsoft.AspNetCore.Mvc;
using ProjectsManager.Application.DTO;
using ProjectsManager.Application.Services.Employees;
using ProjectsManager.Domain.Entities.Frameworks;
using ProjectsManager.ViewModels;

namespace ProjectsManager.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _employeesService;
        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpGet]
        public async Task<IActionResult> AllEmployees()
        {
            var resultService=await _employeesService.GetAll();
            if (resultService.Code == ResultCodes.Failure)
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle="Ошибка вывода сотрудников!";
                errorModel.ErrorDescription = $"Произошла ошибка при выводе сотрудников. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }    
            else
                return View(resultService.Entity);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] EmployeesDto employeeDTO)
        {
            if (ModelState.IsValid)
            {
                var resultService = await _employeesService.AddEmployee(employeeDTO);
                if (resultService.Code == ResultCodes.Success)
                    return Redirect(nameof(AllEmployees));
                else
                {
                    ErrorModel errorModel = new ErrorModel();
                    errorModel.ErrorTitle = "Ошибка при добавлении сотрудника";
                    errorModel.ErrorDescription = $"Произошла ошибка при добавлении сотрудников. {resultService.Message}";
                    return View("~/Views/Error/Error.cshtml", errorModel);
                }
            }
            return View(employeeDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var resultService = await _employeesService.GetById(Id);
            if (resultService.Code == ResultCodes.Failure)
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка при обновлении данных сотрудника";
                errorModel.ErrorDescription = $"Произошла ошибка при обновлении данных сотрудника. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
            else if (resultService.Code == ResultCodes.NotFound)
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка при обновлении данных сотрудника";
                errorModel.ErrorDescription = $"Произошла ошибка при обновлении данных сотрудника. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
            else
                return View(resultService.Entity);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed([FromForm] EmployeesDto employeeDTO)
        {
            if (ModelState.IsValid)
            {
                var resultService = await _employeesService.UpdateEmployee(employeeDTO);
                if (resultService.Code == ResultCodes.Success)
                    return RedirectToAction(nameof(AllEmployees));
                else
                {
                    ErrorModel errorModel = new ErrorModel();
                    errorModel.ErrorTitle = "Ошибка при обновлении данных сотрудника";
                    errorModel.ErrorDescription = $"Произошла ошибка при обновлении данных сотрудника. {resultService.Message}";
                    return View("~/Views/Error/Error.cshtml", errorModel);
                }
            }
            return View(employeeDTO);
        }

        [HttpGet]        
        public async Task<IActionResult> Delete(int Id)
        {
            var resultService = await _employeesService.GetById(Id);
            if (resultService.Code == ResultCodes.Failure)
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка при удалении сотрудника";
                errorModel.ErrorDescription = $"Произошла ошибка при удалении сотрудника. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
            else if (resultService.Code == ResultCodes.NotFound)
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка при удалении сотрудника";
                errorModel.ErrorDescription = $"Произошла ошибка при удалении сотрудника. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
            else
                return View(resultService.Entity);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var resultService = await _employeesService.DeleteEmployee(Id);
            if (resultService.Code == ResultCodes.Success)
                return RedirectToAction(nameof(AllEmployees));
            else
            {
                ErrorModel errorModel = new ErrorModel();
                errorModel.ErrorTitle = "Ошибка при удалении сотрудника";
                errorModel.ErrorDescription = $"Произошла ошибка при удалении сотрудника. {resultService.Message}";
                return View("~/Views/Error/Error.cshtml", errorModel);
            }
        }
    }
}
