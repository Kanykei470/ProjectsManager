using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using ProjectsManager.Domain.Entities.Employees;
using ProjectsManager.Domain.Entities.Projects;

namespace ProjectsManager.Domain.Entities.ProjectMembers
{
    public class ProjectMembersData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ProjectId")]
        public int ProjectId { get; set; }

        [ForeignKey("EmployeesId")]
        public int EmployeesId { get; set; }

        public bool IsProjectManager { get; set; }

        public virtual EmployeesData Employee { get;}
        public virtual ProjectsData Project { get;}
    }
}
