using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Data.DTO;

namespace Todo.Service.Assignment
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IGenericRepository<Assignments> _repository;
        private readonly IMapper _mapper;

        public AssignmentService(IGenericRepository<Assignments> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            return ApiResponseDTO.Success(result.Data, "Silme işlemi başarılı.");

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
            return ApiResponseDTO.Success(result.Data, "Güncelleme başarılı.");
        }

        public ApiResponseDTO FilterByStatus(FilterAssignmentDTO model)
        {
            var result = _repository.Where(x => x.Status.Equals(model.Status));
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed(result.ErrorMessage);
            }
            return ApiResponseDTO.Success(result.Data, "Durum Filtresine Göre Sonuçlar:");
        }

        public ApiResponseDTO FilterByName(FilterAssignmentDTO model)
        {
            var result = _repository.Where(x => x.Name.Contains(model.Name));
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed(result.ErrorMessage);
            }
            return ApiResponseDTO.Success(result.Data, "Arama Sonuçları:");
        }

        public ApiResponseDTO GetAll()
        {
            var result = _repository.GetAll();
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed(result.ErrorMessage);
            }
            return ApiResponseDTO.Success(result.Data, "Tüm Görevler:");
        }
    }
}
