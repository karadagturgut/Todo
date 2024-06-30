using AutoMapper;
using Todo.Core;

namespace Todo.Service
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

        public ApiResponseDTO GetActiveBoards()
        {
            var result = _repository.Where(x => x.Status.Equals(true));
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed("Aktif board listesi getirilirken hata.");
            }

            return ApiResponseDTO.Success(result.Data, "Aktif Board Listesi:");
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
        public ApiResponseDTO GetListedBoards(ListedBoardsDTO model)
        {
            var result = _repository.Where(x=>model.BoardList.Contains(x.Id));
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed("Board Listesi hatası.");
            }
            return ApiResponseDTO.Success(result.Data,"Board seçiniz:");
        }
    }
}
