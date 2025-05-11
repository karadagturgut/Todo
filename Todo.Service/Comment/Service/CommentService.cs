using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core;

namespace Todo.Service
{
    public class CommentService : ICommentService
    {
        private readonly IGenericRepository<AssignmentComment> _assignmentComments;
        public CommentService(IGenericRepository<AssignmentComment> assignmentComments)
        {
            _assignmentComments = assignmentComments;
        }

        public ApiResponseDTO Add(CommentDTO model)
        {
            var entity = CommentMapper.ConvertToEntity(model);
            var result = _assignmentComments.Add(entity);
            if (!result.IsSuccess) { return ApiResponseDTO.Failed("Yorum eklenirken hata."); }
            return ApiResponseDTO.Success(null,"Başarılı şekilde eklendi.");
        }

        public ApiResponseDTO Delete(int id)
        {
            var comment = _assignmentComments.Where(x => x.Id.Equals(id));
            if (!comment.IsSuccess || !comment.Data.Any()) { return ApiResponseDTO.Failed("Yorum bulunamadı."); }
            var result = _assignmentComments.DeleteById(id);
            if (!result.IsSuccess) { return ApiResponseDTO.Failed("Yorum kaldırılamadı."); }
            return ApiResponseDTO.Success(null,"Yorum kaldırıldı.");
        }

        public ApiResponseDTO GetByAssignmentId(int taskId)
        {
            var result = _assignmentComments.Where(x => x.AssignmentId.Equals(taskId));
            if (!result.IsSuccess) { return ApiResponseDTO.Failed("Yorumlar alınırken hata oluştu."); }
            return ApiResponseDTO.Success(result,null);
        }

        public ApiResponseDTO Update(CommentDTO model)
        {
            var comment = _assignmentComments.Where(x => x.Id.Equals(model.Id));
            if (!comment.IsSuccess || !comment.Data.Any()) { return ApiResponseDTO.Failed("Yorum bulunamadı."); }
            var entity = CommentMapper.ConvertToEntity(model);
            var result = _assignmentComments.Update(entity);
            if (!result.IsSuccess) { return ApiResponseDTO.Failed("Güncelleme sırasında hata oluştu."); }
            return ApiResponseDTO.Success(null, "Başarıyla güncellendi.");
        }
    }
}
