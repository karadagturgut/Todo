using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Core;

namespace Todo.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]/[action]")]
    [ApiController]
    public class AssignmentController : BaseController
    {
        private readonly IAssignmentService _service;

        public AssignmentController(IAssignmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Statuses() 
        {
            var result = _service.GetAssignmentStatuses();
            return ApiResponse(result);
        }

        [HttpPost]
        public IActionResult GetAll(FilterAssignmentDTO model)
        {
            var result = _service.FilterByBoardId(model);
            return ApiResponse(result);  
        }

        [HttpPost]
        public IActionResult Add(CreateAssignmentDTO model)
        {
            var result = _service.Add(model);
            return ApiResponse(result);
        }

        [HttpPatch]
        public IActionResult Update(UpdateAssignmentDTO model)
        {
            var result = _service.Update(model);
            return ApiResponse(result);
        }

        [HttpDelete]
        public IActionResult Delete(DeleteAssignmentDTO model)
        {
            var result = _service.Delete(model);
            return ApiResponse(result);
        }

        [HttpPost]
        public IActionResult FilterByStatus(FilterAssignmentDTO model)
        {
            var result = _service.FilterByStatus(model);
            return ApiResponse(result);
        }
        [HttpPost]
        public IActionResult FilterByName(FilterAssignmentDTO model)
        {
            var result = _service.FilterByName(model);
            return ApiResponse(result);
        }

    }
}
