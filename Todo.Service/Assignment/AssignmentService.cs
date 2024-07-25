using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Todo.Core;

namespace Todo.Service
{
    public partial class AssignmentService : IAssignmentService
    {
        private readonly IGenericRepository<Assignments> _repository;
        private readonly IGenericRepository<AssignmentStatus> _statusRepository;
        private readonly IGenericRepository<TodoUser> _userRepository;
        private readonly IMapper _mapper;
        private readonly CacheService _cacheService;
        public AssignmentService(IGenericRepository<Assignments> repository, IMapper mapper, IGenericRepository<AssignmentStatus> statusRepository, CacheService cacheService, IGenericRepository<TodoUser> userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _statusRepository = statusRepository;
            _cacheService = cacheService;
            _userRepository = userRepository;
        }

        public ApiResponseDTO Add(CreateAssignmentDTO model)
        {
            var mapped = _mapper.Map<Assignments>(model);
            var result = _repository.Add(mapped);
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed(result.ErrorMessage);
            }
            return ApiResponseDTO.Success(result.Data, "Ekleme başarılı.");
        }

        public ApiResponseDTO Delete(DeleteAssignmentDTO model)
        {
            var result = _repository.DeleteById(model.Id);
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed(result.ErrorMessage);
            }
            return ApiResponseDTO.Success(null, "Silme işlemi başarılı.");

        }

        public ApiResponseDTO Update(UpdateAssignmentDTO model)
        {
            var existingRecord = _repository.GetById(model.Id).Data;
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
            return ApiResponseDTO.Success(null, "Güncelleme başarılı.");
        }


        /// <summary>
        /// Bir board'da bulunan tüm işleri listeler.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiResponseDTO FilterByBoardId(FilterAssignmentDTO model)
        {

            var assignment = _repository.Where(x => x.BoardId.Equals(model.BoardId)).Data?.AsNoTracking().ToList();
            var status = _statusRepository.GetAll()?.Data?.AsNoTracking().ToList();
            var users = _userRepository.GetAll()?.Data?.AsNoTracking().ToList();
            return ApiResponseDTO.Success(JoinedResult(assignment, status, users), "Bu Board'a Ait Tüm İşler:");
        }


        /// <summary>
        /// Bir board'da bulunan işleri, iş durumuna göre filtreler.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiResponseDTO FilterByStatus(FilterAssignmentDTO model)
        {
            string cacheKey = $"{model.UserId}_Assignment_Filter_Status_{model.Status ?? -1}_Board_{model.BoardId ?? -1}";
            var cachedResponse = _cacheService.GetByCacheKey(cacheKey);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }

            var assignment = _repository.Where(x => x.Status.Equals(model.Status) && x.BoardId.Equals(model.BoardId)).Data?.AsNoTracking().ToList();
            var status = _statusRepository.GetAll()?.Data?.AsNoTracking().ToList();
            var users = _userRepository.GetAll()?.Data?.AsNoTracking().ToList();
            return _cacheService.SetCacheAndGetResponse(cacheKey, JoinedResult(assignment, status,users), "Durum Filtresine Göre Sonuçlar:");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiResponseDTO FilterByName(FilterAssignmentDTO model)
        {
            string cacheKey = $"{model.UserId}_Assignments_By_Name_{model.Name}";
            var cachedResponse = _cacheService.GetByCacheKey(cacheKey);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }

            var assignment = _repository.Where(x => x.Name.Contains(model.Name!) && x.BoardId.Equals(model.BoardId)).Data?.AsNoTracking().ToList();
            var status = _statusRepository.GetAll()?.Data?.AsNoTracking().ToList();
            var users = _userRepository.GetAll()?.Data?.AsNoTracking().ToList();
            return _cacheService.SetCacheAndGetResponse(cacheKey, JoinedResult(assignment, status,users), "Arama Sonuçları:");
        }

        public ApiResponseDTO GetAssignmentStatuses()
        {
            string cacheKey = $"AssignmentStatus";
            var cachedResponse = _cacheService.GetByCacheKey(cacheKey);

            if (cachedResponse != null)
            {
                return cachedResponse;
            }

            var response = _statusRepository.GetAll();
            if (!response.IsSuccess)
            {
                return ApiResponseDTO.Failed(response.ErrorMessage);
            }

            return _cacheService.SetCacheAndGetResponse(cacheKey, response.Data.ToList(), "Durum Listesi:");
        }

        #region Helper

        /// <summary>
        /// Assignment ve AssignmentStatus arasında join yapan metod.
        /// </summary>
        /// <param name="assignments"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private object? JoinedResult(List<Assignments> assignments, List<AssignmentStatus> status, List<TodoUser> users)
        {
            return assignments?
           .Join(status, assignment => assignment.Status, status => status.Id,
               (assignment, status) => new
               {
                   Id = assignment.Id,
                   Name = assignment.Name,
                   Description = assignment.Description,
                   BoardId = assignment.BoardId,
                   Status = status.Name,
                   Assignee = assignment.Assignee,
                   StartDate = assignment.StartDate,
                   EndDate = assignment.EndDate
               })
           .Join(users, assignmentStatus => assignmentStatus.Assignee, user => user.Id,
               (assignmentStatus, user) => new
               {
                   Id = assignmentStatus.Id,
                   Name = assignmentStatus.Name,
                   Description = assignmentStatus.Description,
                   BoardId = assignmentStatus.BoardId,
                   Status = assignmentStatus.Status,
                   AssigneeName = user.UserName,
                   StartDate = assignmentStatus.StartDate,
                   EndDate = assignmentStatus.EndDate
               })
           .ToList();
        }

        #endregion
    }
}
