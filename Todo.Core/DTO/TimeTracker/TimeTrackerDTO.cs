using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.DTO
{
    public class TimeTrackerDTO : BaseDTO
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public string Comment { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
