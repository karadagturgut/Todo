using AutoMapper;
using Todo.Core;

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
