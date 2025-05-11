using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Entity;
using Todo.Core.Entity.Assignments;

namespace Todo.Core
{
    public class TodoUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? OrganizationId { get; set; }
        public virtual Organization? Organization { get; set; }
        public virtual ICollection<AssignmentUser>? Assignments { get; set; }
    }
}
