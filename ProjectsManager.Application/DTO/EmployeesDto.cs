using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManager.Application.DTO
{
    public class EmployeesDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя сотрудника обязательное поле!")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилия сотрудника обязательное поле!")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Отчество сотрудника обязательное поле!")]
        public string MiddleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле Email обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Неверный формат Email")]
        public string Email { get; set; } = string.Empty;

    }
}
