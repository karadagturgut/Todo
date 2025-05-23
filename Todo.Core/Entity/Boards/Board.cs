﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Entity;

namespace Todo.Core
{
    [Table("Boards")]
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; } = true;
        public int OrganizationId { get; set; }
    }
}
