using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Entity.Base;

namespace Todo.Core
{
    public class AssignmentComment : BaseEntity
    {
        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }
        public string CommentText { get; set; }
    }
}
