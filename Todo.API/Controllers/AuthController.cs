using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;

namespace Todo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var result = await _authService.Login(model);
            return ApiResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register(AuthDTO model)
        {
            var result = await _authService.Register(model);
            return ApiResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            var result = await _authService.ChangePassword(model);
            return ApiResponse(result);
        }


        [HttpGet]
        [Route("/GoogleLogin")]
        public async Task<IActionResult> LoginGoogle()
        {
            var cookieValue = HttpContext.Request.Cookies[Environment.GetEnvironmentVariable("GLoginSetter")];

            if (cookieValue == null)
            {
                var properties = new AuthenticationProperties
                {
                    RedirectUri = Url.Action("SignInGoogle")
                };
                return Challenge(properties, GoogleDefaults.AuthenticationScheme);
            }

            var response = await HttpContext.AuthenticateAsync(Environment.GetEnvironmentVariable("GLoginSetter"));
            if (response.Principal == null) { Response.Cookies.Delete(Environment.GetEnvironmentVariable("GLoginSetter")); return RedirectToAction("LoginGoogle"); }

            AuthDTO DTO = new()
            {
                // burada surname hata veriyor. bak.
                Name = response.Principal.FindFirstValue(ClaimTypes.Name),
                Surname = response.Principal.FindFirstValue(ClaimTypes.Surname),
                UserName = response.Principal.FindFirstValue(ClaimTypes.Email),
                EMail = response.Principal.FindFirstValue(ClaimTypes.Email)
            };
            var result = await _authService.RegisterExternalService(DTO);
            return ApiResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _authService.UserProfile(userId);
            return ApiResponse(result);
        }
    }
}
