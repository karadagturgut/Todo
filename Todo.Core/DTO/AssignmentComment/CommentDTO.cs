using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }
        public string CreatedDate { get; set; }
        public string? UpdateDate { get; set; }
    }
    public class DeleteCommentDTO
    {
        public int Id { get; set; }
    }
    public class GetCommentDTO
    {
        public int AssignmentId { get; set; }
    }
}
