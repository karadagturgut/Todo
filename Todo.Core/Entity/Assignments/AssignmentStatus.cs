﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    [Table("AssignmentStatus")]
    public class AssignmentStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
