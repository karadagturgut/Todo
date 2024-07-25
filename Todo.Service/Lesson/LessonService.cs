using AutoMapper;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core;
using Todo.Core.Entity;

namespace Todo.Service
{
    public class LessonService : ILessonService
    {
        private readonly IGenericRepository<Lesson> _repository;
        private readonly IMapper _mapper;

        public LessonService(IGenericRepository<Lesson> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ApiResponseDTO AddLesson(AddLessonDTO model)
        {
            var mapped = _mapper.Map<Lesson>(model);
            var result = _repository.Add(mapped);
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed("Ders ekleme servisinde hata.");
            }
            return ApiResponseDTO.SuccessAdded(null, "Ders eklendi!");
        }

        public ApiResponseDTO DeleteLesson(DeleteLessonDTO model)
        {
            var result = _repository.DeleteById(model.Id);
            if (!result.IsSuccess)
            {
                return ApiResponseDTO.Failed("Ders silme servisinde hata.");
            }
            return ApiResponseDTO.Success(null,$"{model.Id} numaralı ders silindi.");
        }

        public ApiResponseDTO GetByUnit(LessonUnitDTO model)
        {
            throw new NotImplementedException();
        }

        public ApiResponseDTO GetLessonById(DeleteLessonDTO model)
        {
            var result = _repository.GetById(model.Id);
            if (!result.IsSuccess) 
            {
                return ApiResponseDTO.Failed("Ders servisinde hata.");
            }
            return ApiResponseDTO.Success(result,"Ders içeriği:");
        }

        public ApiResponseDTO UpdateLesson(UpdateLessonDTO model)
        {
            var result = _repository.Update(_mapper.Map<Lesson>(model));
            if (!result.IsSuccess) 
            {
                return ApiResponseDTO.Failed("Ders güncelleme servisinde hata.");
            }
            return ApiResponseDTO.Success(null,"Ders güncellendi!");
        }
    }
}
