using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Core;
using Todo.Core.DTO.Auth;

namespace Todo.API.Controllers.Auth
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserRoleController : BaseController
    {

        private readonly IAuthService _authService;

        public UserRoleController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(Name = "SetRoles")]
        public async Task<IActionResult> SetRoles(RoleDTO model)
        {
            var us = await _authService.AddRole(model);
            return Ok(us);
        }
    }
}
