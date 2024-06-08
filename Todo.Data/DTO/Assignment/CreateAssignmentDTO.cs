using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Data.DTO
{
    public class CreateAssignmentDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int BoardId { get; set; }
        public int? Status { get; set; } = 1;
    }
}
