using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Core;
using Todo.Core.DTO;

namespace Todo.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]/[action]")]
    [ApiController]
    public class TimeTrackerController : BaseController
    {
        private readonly ITimeTrackerService _service;

        public TimeTrackerController(ITimeTrackerService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add(TimeTrackerDTO model)
        {
            var example = _service.Add(model);  
            return ApiResponse(example);
        }
        
        [HttpPatch]
        public IActionResult Update(TimeTrackerDTO model)
        {
            var result = _service.Update(model);
            return ApiResponse(result);
        }

        [HttpPost]
        public IActionResult Get(TimeTrackerDTO model)
        {
            var result = _service.Get(model);
            return ApiResponse(result);
        }

        [HttpPost]
        public IActionResult Delete(TimeTrackerDTO model)
        {
            var result = _service.Delete(model);
            return ApiResponse(result);
        }
    }
}
