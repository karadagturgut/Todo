using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Data.DTO;
using Todo.Service.Assignment;

namespace Todo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AssignmentController : ControllerBase
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
            return Ok(result);
        }

        [HttpPost]
        public IActionResult GetAll(FilterAssignmentDTO model)
        {
            var result = _service.FilterByBoardId(model);
            return Ok(result);  
        }

        [HttpPost]
        public IActionResult Add(CreateAssignmentDTO model)
        {
            var result = _service.Add(model);
            return Ok(result);
        }

        [HttpPatch]
        public IActionResult Update(UpdateAssignmentDTO model)
        {
            var result = _service.Update(model);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(DeleteAssignmentDTO model)
        {
            var result = _service.Delete(model);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult FilterByStatus(FilterAssignmentDTO model)
        {
            var result = _service.FilterByStatus(model);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult FilterByName(FilterAssignmentDTO model)
        {
            var result = _service.FilterByName(model);
            return Ok(result);
        }

    }
}
