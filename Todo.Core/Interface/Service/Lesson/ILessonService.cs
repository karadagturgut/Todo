using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public interface ILessonService
    {
        ApiResponseDTO AddLesson(AddLessonDTO model);
        ApiResponseDTO DeleteLesson(DeleteLessonDTO model);
        ApiResponseDTO GetLessonById(DeleteLessonDTO model);
        ApiResponseDTO UpdateLesson(UpdateLessonDTO model);
        ApiResponseDTO GetByUnit(LessonUnitDTO model);
    }
}
