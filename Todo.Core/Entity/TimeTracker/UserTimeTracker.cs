using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Entity.Base;

namespace Todo.Core.Entity
{
    public class UserTimeTracker : MultiTenantEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual TodoUser User { get; set; }
        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }
        public string Comment { get; set; }
        public decimal TimeSpent { get; set; }
        public DateTime LoggedDate { get; set; }
    }
}
