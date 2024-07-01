using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public class GetUsersBoardDTO
    {
        public int UserId { get; set; }
    }
    public class UsersBoardDTO
    {
        public int UserId { get; set; }
        public int BoardId { get; set; }
    }
}
