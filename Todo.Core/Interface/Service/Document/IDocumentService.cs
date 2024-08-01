using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.Interface
{
    public interface IDocumentService
    {
        Task<ApiResponseDTO> AddDocument(AddDocumentDTO model);
        Task<ApiResponseDTO> DeleteDocument(DocumentOperationDTO model);
        Task<ApiResponseDTO> GetDocument(DocumentOperationDTO model);

    }
}
