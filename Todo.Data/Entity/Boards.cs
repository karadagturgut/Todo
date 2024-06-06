using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Data.Entity
{
    [Table("Boards")]
    public class Boards
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; } = true;
    }
}
