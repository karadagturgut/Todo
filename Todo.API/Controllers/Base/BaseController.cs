using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Core;

namespace Todo.API.Controllers
{
   
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult ApiResponse (ApiResponseDTO response)
        {
            return StatusCode(response.StatusCode, response);
        }
    }
}
