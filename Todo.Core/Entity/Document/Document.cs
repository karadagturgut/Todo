using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    [Table("Documents")]
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public Guid FileId { get; set; }
        public int OrganizationId { get; set; }
        public int BoardId { get; set; }
        public int UploadUserId { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
