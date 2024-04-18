using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManager.Application.DTO
{
    public class ProjectsMembersDto
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string? ProjectName { get; set; }

        public int EmployeesId { get; set; }

        public string? EmployeeName { get; set; }

        public bool IsProjectManager { get; set; }
    }
}
