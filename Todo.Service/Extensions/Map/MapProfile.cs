using AutoMapper;
using Todo.Data;
using Todo.Data.DTO;
using Todo.Data.DTO.Board;
using Todo.Data.Entity;

namespace Todo.Service.Extensions.Map
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region Assignments
            CreateMap<Assignments, CreateAssignmentDTO>().ReverseMap();
            CreateMap<Assignments, UpdateAssignmentDTO>().ReverseMap()
             .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            #region Boards
            CreateMap<Boards, CreateBoardDTO>().ReverseMap();
            CreateMap<Boards, UpdateBoardDTO>().ReverseMap()
             .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

        }
    }
}
