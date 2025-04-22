using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Core;
using Todo.Core.DTO;
using Todo.Web.Models.Auth;

namespace Todo.Web.Controllers.Auth
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IOrganizationService _organizationService;

        public AuthController(IAuthService authService, IOrganizationService organizationService)
        {
            _authService = authService;
            _organizationService = organizationService;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            LoginDTO request = new(model.UserName, model.Password);
            var result = await _authService.BackOfficeLogin(request);

            if (!result.IsSuccess || result.Data == null)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Giriş başarısız.");
                return View(model);
            }

            dynamic userData = result.Data as UserInfoDTO;

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, userData.UserId),
            new Claim(ClaimTypes.Name, userData.UserName),
            new Claim(ClaimTypes.Email, userData.EMail),
            new Claim(ClaimTypes.Role, userData.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Board");
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //todo:
            // önce kullanıcı oluştu, sonra organizasyon oluştu. organizasyon için bir de durum eklemek lazım, onay için.
            // register'ı ikiye ayrıabiliriz:
            // 1. mobil: bo'dan org. oluşturulacak ve org ismi alınacak.
            // 2. bo : bo'den de oluşturulurken org. onaya tabii olacak. buradan org. oluşurken, admin user'da oluşacak.

            // şimdiki yapı :
            // önce org. oluşuyor. success ise admin kullanıcısı da oluşuyor.

            OrganizationDTO organization = new() { Name = model.OrganizationName };
            var orgResult = _organizationService.AddOrganization(organization);
            if (!orgResult.IsSuccess)
            {
                return View(model); // toastr veya hata mesajını fırlatacak bir yol bakmalıyım...
            }

            AuthDTO request = new() { UserName = model.UserName, Password = model.Password, EMail = model.EMail, Name = model.Name, Surname = model.Surname, PhoneNumber = model.PhoneNumber, };
            var result = await _authService.Register(request);
            if (!result.IsSuccess)
            {
                return View(model); // toastr veya hata mesajını fırlatacak bir yol bakmalıyım...
            }


            return RedirectToAction("Index", "Home");
        }

    }
}
