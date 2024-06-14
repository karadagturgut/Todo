using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public interface IAssignmentService
    {
        ApiResponseDTO Add(CreateAssignmentDTO model);
        ApiResponseDTO Update(UpdateAssignmentDTO model);
        ApiResponseDTO Delete(DeleteAssignmentDTO model);
        ApiResponseDTO FilterByStatus(FilterAssignmentDTO model);
        ApiResponseDTO FilterByName(FilterAssignmentDTO model);
        ApiResponseDTO FilterByBoardId(FilterAssignmentDTO model);
        ApiResponseDTO GetAssignmentStatuses();
    }
}
