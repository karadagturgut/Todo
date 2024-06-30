using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public class UserBoard
    {
        [Key]
        public int Id { get; set; }
        public int BoardId { get; set; }
        public int UserId { get; set; }
    }
}
