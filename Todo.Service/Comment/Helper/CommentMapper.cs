using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core;

namespace Todo.Service
{
    public static class CommentMapper
    {
        public static AssignmentComment ConvertToEntity(CommentDTO dto)
        {
            if (!DateTime.TryParse(dto.CreatedDate, out DateTime createdDate))
            {
                throw new ArgumentException("Tarih geçersiz formatta gönderildi.");
            }

            DateTime? updateDate = null;
            if (!string.IsNullOrEmpty(dto.UpdateDate))
            {
                if (!DateTime.TryParse(dto.UpdateDate, out DateTime parsedUpdateDate))
                {
                    throw new ArgumentException("Tarih geçersiz formatta gönderildi.");
                }
                updateDate = parsedUpdateDate;
            }

            return new AssignmentComment
            {
                Id = dto.Id,
                AssignmentId = dto.TaskId,
                CommentText = dto.CommentText
            };
        }
    }

}
