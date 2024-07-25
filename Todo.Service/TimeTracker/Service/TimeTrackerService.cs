using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core;
using Todo.Core.DTO;
using Todo.Core.Entity;


namespace Todo.Service
{
    public class TimeTrackerService : ITimeTrackerService
    {
        private readonly IGenericRepository<UserTimeTracker> _repository;

        public TimeTrackerService(IGenericRepository<UserTimeTracker> repository)
        {
            _repository = repository;
        }

        public ApiResponseDTO Add(TimeTrackerDTO model)
        {
            var entity  = TimeTrackerHelper.ConvertToEntity(model);
            var result = _repository.Add(entity);
            if (!result.IsSuccess) { return ApiResponseDTO.Failed("Veri gönderilirken hata oluştu."); }
            return ApiResponseDTO.Success(entity,"Ekleme tamamlandı.");
        }

        public ApiResponseDTO Delete(TimeTrackerDTO model)
        {
            var result = _repository.DeleteById(model.Id);
            if (!result.IsSuccess) { return ApiResponseDTO.Failed("Silme işlemi esnasında hata."); }
            return ApiResponseDTO.Success(null,"Silme işlemi tamamlandı.");
        }

        public ApiResponseDTO Get(TimeTrackerDTO model)
        {
            var result = _repository.Where(x=>x.UserId.Equals(model.UserId));
            if (!result.IsSuccess) { return ApiResponseDTO.Failed("Log getirilmesi işlemi esnasında hata."); }
            return ApiResponseDTO.Success(result,"Log listesi");
        }

        public ApiResponseDTO Update(TimeTrackerDTO model)
        {
            var entity = TimeTrackerHelper.ConvertToEntity(model);
            var result = _repository.Update(entity);
            if (!result.IsSuccess) { return ApiResponseDTO.Failed("Güncelleme işlemi esnasında hata."); }
            return ApiResponseDTO.Success(entity,"Güncelleme işlemi tamamlandı.");
        }
    }
}
