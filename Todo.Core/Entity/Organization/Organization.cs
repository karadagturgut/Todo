using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Entity.Base;

namespace Todo.Core.Entity
{
    [Index(nameof(Name), IsUnique = true)]
    public class Organization : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Board> Boards { get; set; }
        public virtual ICollection<TodoUser> Users { get; set; }
    }
}
