using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Data.DTO;
using Todo.Data.Entity;

namespace Todo.Service.Assignment
{
    public partial class AssignmentService : IAssignmentService
    {
        private readonly IGenericRepository<Assignments> _repository;
        private readonly IGenericRepository<AssignmentStatus> _statusRepository;
        private readonly IMapper _mapper;
        private readonly CacheService _cacheService;
        public AssignmentService(IGenericRepository<Assignments> repository, IMapper mapper, IGenericRepository<AssignmentStatus> statusRepository, CacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _statusRepository = statusRepository;
            _cacheService = cacheService;
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
            return ApiResponseDTO.Success(JoinedResult(assignment, status), "Bu Board'a Ait Tüm İşler:");
        }


        /// <summary>
        /// Bir board'da bulunan işleri, iş durumuna göre filtreler.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiResponseDTO FilterByStatus(FilterAssignmentDTO model)
        {
            string cacheKey = $"Assignment_Filter_Status_{model.Status ?? -1}_Board_{model.BoardId ?? -1}";
            var cachedResponse = _cacheService.GetByCacheKey(cacheKey);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }

            var assignment = _repository.Where(x => x.Status.Equals(model.Status) && x.BoardId.Equals(model.BoardId)).Data?.AsNoTracking().ToList();
            var status = _statusRepository.GetAll()?.Data?.AsNoTracking().ToList();
            return _cacheService.SetCacheAndGetResponse(cacheKey, JoinedResult(assignment, status), "Durum Filtresine Göre Sonuçlar:");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiResponseDTO FilterByName(FilterAssignmentDTO model)
        {
            string cacheKey = $"Assignments_By_Name_{model.Name}";
            var cachedResponse = _cacheService.GetByCacheKey(cacheKey);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }

            var assignment = _repository.Where(x => x.Name.Contains(model.Name!) && x.BoardId.Equals(model.BoardId)).Data?.AsNoTracking().ToList();
            var status = _statusRepository.GetAll()?.Data?.AsNoTracking().ToList();

            return _cacheService.SetCacheAndGetResponse(cacheKey, JoinedResult(assignment, status), "Arama Sonuçları:");
        }

        public ApiResponseDTO GetAssignmentStatuses()
        {

            var response = _statusRepository.GetAll();
            if (!response.IsSuccess)
            {
                return ApiResponseDTO.Failed(response.ErrorMessage);
            }
            return ApiResponseDTO.Success(response.Data, null);
        }

        #region Helper

        /// <summary>
        /// Assignment ve AssignmentStatus arasında join yapan metod.
        /// </summary>
        /// <param name="assignments"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private object? JoinedResult(List<Assignments> assignments, List<AssignmentStatus> status)
        {
            return assignments?.Join(status, assignments => assignments.Status, status => status.Id,
                (assignments, status) => new
                {
                    Id = assignments.Id,
                    Name = assignments.Name,
                    Description = assignments.Description,
                    BoardId = assignments.BoardId,
                    Status = status.Name
                }
                ).ToList();
        }

        #endregion
    }
}
