using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public class FilterAssignmentDTO : BaseDTO
    {
        public int? Status { get; set; }
        public string? Name { get; set; }
        public int? BoardId { get; set; }
    }
}
