using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Todo.Core
{
    public class BaseDTO
    {
        public int? UserId { get; set; }

        //public int OrganizationId { get; set; }
    }
}
