using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core;

namespace Todo.Service
{
    public class UserBoardService : IUserBoardService
    {
        private readonly IGenericRepository<UserBoard> _repository;
        private readonly IMapper _mapper;
        public UserBoardService(IGenericRepository<UserBoard> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ApiResponseDTO AddUserBoard(UsersBoardDTO model)
        {
            var result = _repository.Add(_mapper.Map<UserBoard>(model));
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed("Kullanıcı - Board ilişkilendirme sırasında hata.");
            }
            return ApiResponseDTO.SuccessAdded(null,"Kullanıcı board'a eklendi.");
        }

        public ApiResponseDTO BoardsByUserId(GetUsersBoardDTO model)
        {
            var result = _repository.Where(x=>x.UserId.Equals(model.UserId));
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed("Board Id Listesi Getirilirken Hata Oluştu");
            }
            return ApiResponseDTO.Success(result.Data.Select(x=>x.BoardId).ToList(),"Board Id Listesi:");
        }

        public ApiResponseDTO RemoveUserBoard(UsersBoardDTO model)
        {
            var deleted = _repository.Where(x=>x.BoardId.Equals(model.BoardId) && x.UserId.Equals(model.UserId));
            if (!deleted.IsSuccess) { return ApiResponseDTO.Failed("Kullanıcı - Board ilişkilendirme sırasında hata."); }
            var result = _repository.DeleteById(deleted.Data.FirstOrDefault().Id);
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed("Kullanıcı - Board ilişkilendirme sırasında hata.");
            }
            return ApiResponseDTO.SuccessAdded(null, "Kullanıcı board'dan kaldırıldı.");
        }

        public ApiResponseDTO UsersByBoardId(UsersBoardDTO model)
        {
            var result = _repository.Where(x => x.BoardId.Equals(model.BoardId));
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed("User Id listesi getirilirken hata.");
            }
            return ApiResponseDTO.Success(result.Data.Select(x=>x.UserId).ToList(),"UserId Listesi:");
        }
    }
}
