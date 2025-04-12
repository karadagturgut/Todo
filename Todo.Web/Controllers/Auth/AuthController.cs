using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Core;
using Todo.Web.Models.Auth;

namespace Todo.Web.Controllers.Auth
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Login(LoginViewModel model)
        {
            LoginDTO request = new(model.UserName,model.Password);
            var result = await _authService.Login(request);
            if (!result.IsSuccess)
            {
                return View(); // toastr veya hata mesajını fırlatacak bir yol bakmalıyım...
            }
           return RedirectToAction("Index","Home");
        }
    }
}
