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

        public UserBoardService(IGenericRepository<UserBoard> repository)
        {
            _repository = repository;
        }

        public ApiResponseDTO BoardsByUserId(GetUsersBoardDTO model)
        {
            var result = _repository.Where(x=>x.UserId.Equals(model.UserId));
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed("Board Id Listesi Getirilirken Hata Oluştu");
            }
            return ApiResponseDTO.Success(result.Data.Select(x=>x.Id).ToList(),"Board Id Listesi:");
        }

        public ApiResponseDTO UpdateUserBoard(UpdateUsersBoardDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
