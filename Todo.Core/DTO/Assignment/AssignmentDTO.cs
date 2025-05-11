using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.DTO.Assignment
{
    public class AssignmentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int BoardId { get; set; }

        public string StatusName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<AssigneeDto> Assignees { get; set; } = new();
    }

    public class AssigneeDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public bool IsOwner { get; set; }
    }
}
