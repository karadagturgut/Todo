using AutoMapper;
using Todo.Core;
using Todo.Core.Entity;

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

            #region User
            CreateMap<TodoUser,AuthDTO>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            #region UserBoard
            CreateMap<UsersBoardDTO, UserBoard>().ReverseMap();
            #endregion

            #region
            CreateMap<AddLessonDTO, Lesson>().ReverseMap();
            CreateMap<UpdateLessonDTO, Lesson>().ReverseMap(); 
            #endregion

        }
    }
}
