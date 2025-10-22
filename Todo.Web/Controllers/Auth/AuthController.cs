using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using Todo.Core;
using Todo.Core.DTO;
using Todo.Core.Interface.Service.OrganizationParticipation;
using Todo.Web.Controllers.Base;
using Todo.Web.Models.Auth;

namespace Todo.Web.Controllers.Auth
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IOrganizationService _organizationService;
        private readonly IStringLocalizer<Lang> _localizer;
        private readonly IOrganizationParticipationService _organizationParticipationService;
        public AuthController(IAuthService authService, IOrganizationService organizationService, IToastNotification notification, IStringLocalizer<Lang> localizer, IOrganizationParticipationService organizationParticipationService) : base(localizer, notification)
        {
            _authService = authService;
            _organizationService = organizationService;
            _localizer = localizer;
            _organizationParticipationService = organizationParticipationService;
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


            // organizasyon var mı?
            OrganizationDTO organization = new() { Name = model.OrganizationName };
            var orgResult = _organizationService.AddOrganization(organization);

            // var ise: kullanıcıyı kaydet, organizasyona katılım isteği göndert:
            if (!orgResult.IsSuccess)
            {
                AuthDTO userRequest = new()
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    EMail = model.EMail,
                    Name = model.Name,
                    Surname = model.Surname,
                    PhoneNumber = model.PhoneNumber,
                    OrganizationId = null
                };

                var userResult = await _authService.Register(userRequest);
                if (!userResult.IsSuccess)
                {
                    return View(model);
                }

                else
                {
                    var participatingOrganization = _organizationService.GetOrganization(new() { Name = model.OrganizationName });

                    if (participatingOrganization.IsSuccess && participatingOrganization.Data is Core.Entity.Organization org)
                    {
                        int organizationId = org.Id;
                        _organizationParticipationService.OrganizationJoinRequest(new() { UserId = (int)userResult.Data, OrganizationId = (int)participatingOrganization.Data });

                        return RedirectToAction("Index", "Home");
                    }

                }

            }

            // değilse: onu ilk kullanıcı kabul et ve admin olarak ata :

            AuthDTO request = new()
            {
                UserName = model.UserName,
                Password = model.Password,
                EMail = model.EMail,
                Name = model.Name,
                Surname = model.Surname,
                PhoneNumber = model.PhoneNumber,
                OrganizationId = (int)orgResult.Data,
                Roles = new() { "User", "Admin" }
            };

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
