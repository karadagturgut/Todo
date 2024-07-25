using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.Entity
{
    public class UserTimeTracker
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AssignmentId { get; set; }
        public string Comment { get; set; }
        public decimal TimeSpent { get; set; }
        public DateTime LoggedDate { get; set; }
    }
}
