using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public class AddLessonDTO
    {
        public string Name { get; set; }
        public int Path { get; set; }
    }
    public class UpdateLessonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }

    public class DeleteLessonDTO
    {
        public int Id { get; set; }
    }

    public class LessonUnitDTO
    {
        public int UnitId { get; set; }
    }
}
