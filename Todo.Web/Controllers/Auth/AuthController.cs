using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Core;
using Todo.Core.DTO;
using Todo.Web.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Todo.Web.Controllers.Base;
using Microsoft.Extensions.Localization;
using NToastNotify;

namespace Todo.Web.Controllers.Auth
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IOrganizationService _organizationService;
        private readonly IStringLocalizer<Lang> _localizer;
        public AuthController(IAuthService authService, IOrganizationService organizationService, IToastNotification notification, IStringLocalizer<Lang> localizer) : base(localizer, notification)
        {
            _authService = authService;
            _organizationService = organizationService;
            _localizer = localizer;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.BackOfficeLogin(new LoginDTO(model.UserName, model.Password));

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Giriş başarısız.");
                return View(model);
            }

            UISuccess(_localizer["LoginSuccess"].Value);

            return RedirectToAction("Index", "Board");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            TempData["LogoutMessage"] = "Başarıyla çıkış yaptınız.";
            return RedirectToAction("Login", "Auth");
        }

    }
}
