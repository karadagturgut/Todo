using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public class UpdateBoardDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }
    }
}
