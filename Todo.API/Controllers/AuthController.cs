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
        [Route("login")]
        public Task LoginGoogle()
        {
            return HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("SignInGoogle")
            });

        }

            [HttpGet("/signin-google")]
            public async Task<IActionResult> GoogleLogin()
            {
                var response = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
                if (response.Principal == null) return BadRequest();

                var name = response.Principal.FindFirstValue(ClaimTypes.Name);
                var givenName = response.Principal.FindFirstValue(ClaimTypes.GivenName);
                var email = response.Principal.FindFirstValue(ClaimTypes.Email);
                //Do something with the claims
                // var user = await UserService.FindOrCreate(new { name, givenName, email});

                return Ok();
            }

        }
    }
