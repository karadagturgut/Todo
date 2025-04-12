using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.Entity
{
    [Index(nameof(Name), IsUnique = true)]
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
