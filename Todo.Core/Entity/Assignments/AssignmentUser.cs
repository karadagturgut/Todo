using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Entity.Base;

namespace Todo.Core.Entity.Assignments
{
    public class AssignmentUser : BaseEntity
    {
        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        public int UserId { get; set; }
        public virtual TodoUser User { get; set; }

        public bool IsOwner { get; set; } = false; 

        public DateTime? AssignedAt { get; set; }
    }
}
