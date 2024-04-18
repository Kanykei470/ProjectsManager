using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsManager.Domain.Entities.ProjectMembers;

namespace ProjectsManager.Domain.Entities.Projects
{
    public class ProjectsData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string CustomerName { get; set; }


        public string ExecutorName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public byte Priority { get; set; }

        public virtual List<ProjectMembersData> ProjectMembers { get;}
    }
}
