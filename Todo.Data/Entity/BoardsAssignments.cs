using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Data.Entity
{
    [Table("BoardsAssignments")]
    public class BoardsAssignments
    {
        [Key]
        public int Id { get; set; }
        public int BoardId { get; set; }
        public int AssignmentId { get; set; }
    }
}
