using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Entity.Base;

namespace Todo.Core
{
    public class UserBoard : BaseEntity
    {
        public int BoardId { get; set; }
        public virtual Board Board { get; set; }

        public int UserId { get; set; }
        public virtual TodoUser User { get; set; }
    }
}
