using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public class ActionRole
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Roles { get; set; }
        public bool IsPublic { get; set; }
    }
}
