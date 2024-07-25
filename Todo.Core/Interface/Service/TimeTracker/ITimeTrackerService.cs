using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.DTO;

namespace Todo.Core
{
    public interface ITimeTrackerService
    {
        ApiResponseDTO Add(TimeTrackerDTO model);
        ApiResponseDTO Update(TimeTrackerDTO model);
        ApiResponseDTO Delete(TimeTrackerDTO model);
        ApiResponseDTO Get(TimeTrackerDTO model);
    }
}
