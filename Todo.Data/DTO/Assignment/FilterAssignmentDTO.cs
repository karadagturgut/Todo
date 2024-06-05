using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Data.DTO
{
    public class FilterAssignmentDTO
    {
        public bool? IsCompleted { get; set; }
        public string? Name { get; set; }
    }
}
