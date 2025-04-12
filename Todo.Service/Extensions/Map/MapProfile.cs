using AutoMapper;
using Todo.Core;
using Todo.Core.DTO;
using Todo.Core.Entity;

namespace Todo.Service.Extensions.Map
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region Assignments
            CreateMap<Assignment, CreateAssignmentDTO>().ReverseMap();
            CreateMap<Assignment, UpdateAssignmentDTO>().ReverseMap()
             .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            #region Boards
            CreateMap<Board, CreateBoardDTO>().ReverseMap();
            CreateMap<Board, UpdateBoardDTO>().ReverseMap()
             .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            #region User
            CreateMap<TodoUser,AuthDTO>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            #region UserBoard
            CreateMap<UsersBoardDTO, UserBoard>().ReverseMap();
            #endregion

            #region Lesson
            CreateMap<AddLessonDTO, Lesson>().ReverseMap();
            CreateMap<UpdateLessonDTO, Lesson>().ReverseMap();
            #endregion

            #region Organization
            CreateMap<OrganizationDTO,Organization>().ReverseMap();
            #endregion
        }
    }
}
