using AutoMapper;
using Todo.Core;
using Todo.Core.Entity;

namespace Todo.Service
{
    public class BoardService : IBoardService
    {
        private readonly IGenericRepository<Board> _repository;
        private readonly IUserBoardService _userBoardService;
        private readonly IMapper _mapper;

        public BoardService(IGenericRepository<Board> repository, IMapper mapper, IUserBoardService userBoardService)
        {
            _repository = repository;
            _mapper = mapper;
            _userBoardService = userBoardService;
        }

        public ApiResponseDTO Add(CreateBoardDTO model)
        {
            var mapped = _mapper.Map<Board>(model);
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

            return ApiResponseDTO.Success(result, "Aktif Board Listesi:");
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
        public ApiResponseDTO GetUserBoards(ListedBoardsDTO model)
        {

            //var result = _repository.Where(x=>model.BoardList.Contains(x.Id)); Syntax near '$ is incorrect. hatası, EF/sql versiyon uyumsuzluğundan dolayı yoruma alındı.

            var boardIdList = _userBoardService.BoardsByUserId(new() { UserId = model.UserId });
            if (!boardIdList.IsSuccess)
            {
                return ApiResponseDTO.Failed("Kullanıcı kayıtlı olduğu board listesi alınamadı.");
            }

            List<Board> result = new();

            if (boardIdList.Data != null)
            {
                foreach (var item in (List<int>)boardIdList.Data)
                {
                    var board = _repository.Where(x => x.Id.Equals(item));

                    if (!board.IsSuccess)
                    {
                        return ApiResponseDTO.Failed("Board Listesi hatası.");
                    }

                    result.Add(board.Data.FirstOrDefault());
                }

            }
            return ApiResponseDTO.Success(result, "Board seçiniz:");
        }

        public ApiResponseDTO GetOrganizationBoards(OrganizationBoardsDTO model)
        {
            var result = _repository.Where(x => x.OrganizationId.Equals(model.OrganizationId));
            if (!result.IsSuccess) { return ApiResponseDTO.Failed("Organizasyona ait board listesi getirilirken hata."); }
            return ApiResponseDTO.Success(result, "Organizasyona ait boardlar:");
        }

        public ApiResponseDTO GetBoard(DeleteBoardDTO model)
        {
            var existingRecord = _repository.GetById(model.BoardId);
            if (!existingRecord.IsSuccess || existingRecord.Data is null) { return ApiResponseDTO.Failed("Kayıt bulunamadı."); }

            var result = _repository.Update(existingRecord.Data);
            if (!result.IsSuccess)
                return ApiResponseDTO.Failed("Kayıt güncellenirken hata.");

            return ApiResponseDTO.Success(existingRecord.Data as Board, "Kayıt güncellendi.");
        }
    }
}
