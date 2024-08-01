using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Todo.Core;
using Todo.Core.Interface;

namespace Todo.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly FileService _fileService;
        private readonly string fileDirectory = "documents";
        public DocumentService(FileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<ApiResponseDTO> AddDocument(AddDocumentDTO model)
        {
            try
            {
                var fileId = Guid.NewGuid();
                var fileResult = await _fileService.UploadFile(fileId, model.File, fileDirectory);
                if (!fileResult.HttpStatusCode.Equals(HttpStatusCode.OK))
                {
                    return ApiResponseDTO.Failed("Dosya bulut sunucuya yüklenirken hata oluştu.");
                }
                return ApiResponseDTO.SuccessAdded(fileId, "Dosya eklendi.");
            }
            catch(Exception ex)
            {
                return ApiResponseDTO.Failed("Dosya bulut sunucuya yüklenirken hata oluştu.");
            }
        }

        public async Task<ApiResponseDTO> DeleteDocument(DocumentOperationDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponseDTO> GetDocument(DocumentOperationDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
