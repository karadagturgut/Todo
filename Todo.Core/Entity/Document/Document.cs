using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Entity.Base;

namespace Todo.Core
{
    public class Document : MultiTenantEntity
    {
        public Guid FileId { get; set; }

        public int BoardId { get; set; }
        public virtual Board Board { get; set; }

        public int UploadUserId { get; set; }
        public virtual TodoUser UploadUser { get; set; }

        /// Eğer bu alan doluysa, döküman sadece ilgili göreve (Assignment) özeldir.
        /// Boşsa, board seviyesinde genel dokümandır.
        public int? AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public long? SizeKb { get; set; }
    }
}
