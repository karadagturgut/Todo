using AutoMapper;
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
    public class OrganizationService : IOrganizationService
    {
        private readonly IGenericRepository<Organization> _repository;
        private readonly IMapper _mapper;

        public OrganizationService(IGenericRepository<Organization> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ApiResponseDTO AddOrganization(OrganizationDTO model)
        {
            var isExists = _repository.Where(x => x.Name.Equals(model.Name));
            if (isExists?.Data?.Count() > 0)
            {
                return ApiResponseDTO.Failed("Aynı isimde organizasyon bulunuyor.");
            }
            var mapped = _mapper.Map<Organization>(model);
            var result = _repository.Add(mapped);
            if (result.IsSuccess) { return ApiResponseDTO.Failed("Kayıt oluşturulurken hata."); }
            return ApiResponseDTO.Success(null, "Kayıt başarılı.");
        }

        public ApiResponseDTO AllOrganizations()
        {
            var result = _repository.GetAll();
            if (!result.IsSuccess) { return ApiResponseDTO.Failed("Organizasyon listesi getirilirken hata oluştu."); }
            return ApiResponseDTO.Success(result, "Organizasyon listesi:");
        }

        public ApiResponseDTO DeleteOrganization(DeleteOrganizationDTO model)
        {
            var result = _repository.DeleteById(model.Id);
            if (!result.IsSuccess) { return ApiResponseDTO.Failed(result.ErrorMessage); }
            return ApiResponseDTO.Success(null, "İşlem başarıyla tamamlandı.");
        }

        public ApiResponseDTO UpdateOrganization(UpdateOrganizationDTO model)
        {
            var existingRecord = _repository.GetById(model.Id);
            if (!existingRecord.IsSuccess || existingRecord.Data is null) { return ApiResponseDTO.Failed("Kayıt bulunamadı."); }
            existingRecord.Data.Name = model.Name;

            var result = _repository.Update(existingRecord.Data);
            if (!result.IsSuccess)
                return ApiResponseDTO.Failed("Kayıt güncellenirken hata.");

            return ApiResponseDTO.Success(result, "Kayıt güncellendi.");
        }
    }
}
