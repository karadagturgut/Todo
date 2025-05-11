using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Entity;
using Todo.Core.Entity.Base;

namespace Todo.Core
{
    public class Board : MultiTenantEntity
    {
        public string Name { get; set; }
        public bool Status { get; set; } = true;
    }
}
