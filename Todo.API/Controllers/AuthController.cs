    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Core;

namespace Todo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginDTO model)
        {
            var result = await _authService.Login(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register(AuthDTO model)
        {
            var result = await _authService.Register(model);
            return Ok(result);
        }
    }
}
