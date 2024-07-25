using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public interface ICommentService
    {
        ApiResponseDTO Add(CommentDTO model);
        ApiResponseDTO Update(CommentDTO model);
        ApiResponseDTO Delete(int id);
        ApiResponseDTO GetByAssignmentId(int taskId);
    }
}
