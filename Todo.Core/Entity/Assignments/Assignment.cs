using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Entity.Assignments;
using Todo.Core.Entity.Base;

namespace Todo.Core
{
    public class Assignment : MultiTenantEntity
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public int BoardId { get; set; }
        public virtual Board Board { get; set; }

        [Required]
        public int StatusId { get; set; }
        public virtual AssignmentStatus Status { get; set; }

        public virtual ICollection<AssignmentUser> AssignedUsers { get; set; }
        public virtual ICollection<AssignmentComment>  AssignmentComments{ get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
  
    }
}
