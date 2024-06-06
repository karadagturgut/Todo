using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class AssignmentService : IAssignmentService
    {
        private readonly IGenericRepository<Assignments> _repository;
        private readonly IGenericRepository<AssignmentStatus> _statusRepository;
        private readonly IMapper _mapper;

        public AssignmentService(IGenericRepository<Assignments> repository, IMapper mapper, IGenericRepository<AssignmentStatus> statusRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _statusRepository = statusRepository;
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

        public ApiResponseDTO FilterByStatus(FilterAssignmentDTO model)
        {
            var result = _repository.Where(x => x.Status.Equals(model.Status) && x.BoardId.Equals(model.BoardId));
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed(result.ErrorMessage);
            }

            var filtered = result.Data?.AsNoTracking();
            var status = _statusRepository.GetAll()?.Data?.AsNoTracking();

            var returnResult = filtered?.Join(status, assignments => assignments.Status, status => status.Id,
                (assignments, status) => new
                {
                    Id = assignments.Id,
                    Name = assignments.Name,
                    Description = assignments.Description,
                    BoardId = assignments.BoardId,
                    Status = status.Name
                }
                ).ToList();

            return ApiResponseDTO.Success(returnResult, "Durum Filtresine Göre Sonuçlar:");
        }

        public ApiResponseDTO FilterByName(FilterAssignmentDTO model)
        {
            var result = _repository.Where(x => x.Name.Contains(model.Name));
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed(result.ErrorMessage);
            }
            var filtered = result.Data?.AsNoTracking();
            var status = _statusRepository.GetAll()?.Data?.AsNoTracking();

            var returnResult = filtered?.Join(status, assignments => assignments.Status, status => status.Id,
                (assignments, status) => new
                {
                    Id = assignments.Id,
                    Name = assignments.Name,
                    Description = assignments.Description,
                    BoardId = assignments.BoardId,
                    Status = status.Name
                }
                ).ToList();
            return ApiResponseDTO.Success(returnResult, "Arama Sonuçları:");
        }

        public ApiResponseDTO GetAll()
        {
            var assignments = _repository.GetAll()?.Data?.AsNoTracking();
            var status = _statusRepository.GetAll()?.Data?.AsNoTracking();


            var result = assignments?.Join(status, assignments => assignments.Status, status => status.Id,
                (assignments, status) => new
                {
                    Id = assignments.Id,
                    Name = assignments.Name,
                    Description = assignments.Description,
                    BoardId = assignments.BoardId,
                    Status = status.Name
                }
                ).ToList();

            return ApiResponseDTO.Success(result, "Tüm Görevler:");
        }
    }
}
