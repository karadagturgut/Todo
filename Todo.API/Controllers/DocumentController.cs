﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Core;
using Todo.Core.Interface;

namespace Todo.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]/[action]")]
    [ApiController]
    public class DocumentController : BaseController
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddDocument(AddDocumentDTO model)
        {
            var result = await _documentService.AddDocument(model);
            return ApiResponse(result);
        }
    }
}
