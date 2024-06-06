using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Data.DTO;
using Todo.Data.DTO.Board;
using Todo.Data.Entity;

namespace Todo.Service.Board
{
    public class BoardService : IBoardService
    {
        private readonly IGenericRepository<Boards> _repository;
        private readonly IMapper _mapper;

        public BoardService(IGenericRepository<Boards> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ApiResponseDTO Add(CreateBoardDTO model)
        {
            var mapped = _mapper.Map<Boards>(model);
            var result = _repository.Add(mapped);
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed(result.ErrorMessage);
            }
            return ApiResponseDTO.Success(result.Data, "Ekleme işlemi başarıyla tamamlandı.");
        }

        public ApiResponseDTO Delete(DeleteBoardDTO model)
        {
            var result = _repository.DeleteById(model.BoardId);
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed(result.ErrorMessage);
            }
            return ApiResponseDTO.Success(null, "Silme işlemi başarıyla tamamlandı.");
        }

        public ApiResponseDTO GetAll()
        {
            var result = _repository.GetAll();
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed(result.ErrorMessage);
            }
            return ApiResponseDTO.Success(result.Data, "Tüm Boardlar Listesi:");
        }

        public ApiResponseDTO Update(UpdateBoardDTO model)
        {
            // Var olan kaydı alıyoruz.
            var existingRecord = _repository.GetById((int)model.Id!).Data;
            if (existingRecord == null)
            {
                return ApiResponseDTO.Failed("Kayıt bulunamadı.");
            }

            // Sadece gerekli alanları güncellemek için eşleme yapılıyor.
            _mapper.Map(model, existingRecord);
            var result = _repository.Update(existingRecord);
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed(result.ErrorMessage);
            }
            return ApiResponseDTO.Success(null, "Güncelleme işlemi başarıyla tamamlandı.");
        }
    }
}
