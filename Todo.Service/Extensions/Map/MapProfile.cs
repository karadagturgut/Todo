using AutoMapper;
using Todo.Data;
using Todo.Data.DTO;

namespace Todo.Service.Extensions.Map
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Assignments, CreateAssignmentDTO>().ReverseMap();
            CreateMap<Assignments, UpdateAssignmentDTO>().ReverseMap();
        }
    }
}
