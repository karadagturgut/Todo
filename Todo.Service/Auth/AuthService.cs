using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using Todo.Core;

namespace Todo.Service.Auth
{
    public partial class AuthService : IAuthService
    {
        private readonly UserManager<TodoUser> _userManager;
        private readonly RoleManager<TodoRole> _roleManager;
        private readonly IGenericRepository<ActionRole> _actionRoleRepository;
        private readonly IMapper _mapper;
        private readonly EndpointDataSource _endpointDataSource;
        private readonly SignInManager<TodoUser> _signInManager;
        public AuthService(UserManager<TodoUser> userManager, RoleManager<TodoRole> roleManager, IMapper mapper, IGenericRepository<ActionRole> actionRoleRepository, EndpointDataSource endpointDataSource, SignInManager<TodoUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _actionRoleRepository = actionRoleRepository;
            _endpointDataSource = endpointDataSource;
            _signInManager = signInManager;
        }


        public ApiResponseDTO Authorize(AuthorizeDTO model)
        {
            // Veritabanı sorgusu
            var actionRoleEntries = _actionRoleRepository.Where(ar => ar.Action == model.Path);

            // Veritabanı erişim hatası
            if (!actionRoleEntries.IsSuccess)
            {
                return ApiResponseDTO.Unauthorized("Erişim yetkilerine erişimde hata.");
            }

            // Null kontrolü
            var rolesData = actionRoleEntries.Data;
            if (rolesData == null || !rolesData.Any())
            {
                return ApiResponseDTO.Unauthorized("Erişim Reddedildi: Yetkiniz yok.");
            }

            // IsPublic kontrolü
            if (rolesData.Any(ar => ar.IsPublic))
            {
                return ApiResponseDTO.Success(null, "Kullanıcı yetkili.");
            }

            // Kullanıcı rollerinin kontrolü
            var hasValidRole = rolesData.Any(x => x.Roles.Contains(model.Role));
            if (hasValidRole)
            {
                return ApiResponseDTO.Success(null, "Kullanıcı yetkili.");
            }

            // Yetkisiz erişim
            return ApiResponseDTO.Unauthorized("Erişim Reddedildi: Yetkiniz yok.");
        }

        public IEnumerable<ActionRole> GetActionRolesByPath(string path)
        {
            return _actionRoleRepository.Where(ar => ar.Action == path).Data;
        }

        public async Task<ApiResponseDTO> ChangePassword(ChangePasswordDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null) { return ApiResponseDTO.NoContent(null, "Kullanıcı bulunamadı."); }

            var changePassword = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
            if (!changePassword.Succeeded) { return ApiResponseDTO.NoContent(null, "Şifre uyumsuzluğu var. Lütfen kontrol ediniz."); }

            return ApiResponseDTO.Success(null, "İşlem başarıyla tamamlandı.");
        }

        public async Task<ApiResponseDTO> Login(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = ServiceHelper.JwtTokenProvider(user, UserRoles(user).Result);
                return ApiResponseDTO.Success(token, null);
            }
            return ApiResponseDTO.Failed("Kullanıcı adı ya da şifre hatalı.");
        }

        public async Task<ApiResponseDTO> BackOfficeLogin(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                await _signInManager.SignInAsync(user, isPersistent: true); 

                return ApiResponseDTO.Success(null, "Login başarılı");
            }

            return ApiResponseDTO.Failed("Kullanıcı adı ya da şifre hatalı.");
        }

        //todo: bo register ayrılabilir mi?
        public async Task<ApiResponseDTO> Register(AuthDTO model)
        {
            List<string> roles = new() { "User" };
            var user = _mapper.Map<TodoUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var checkRoles = await CheckRoles(roles);
                if (checkRoles)
                {
                    await _userManager.AddToRolesAsync(user, roles);
                    return ApiResponseDTO.Success(user.Id, "Kullanıcı başarıyla oluşturuldu.");
                }
                return ApiResponseDTO.Failed("Rol atama sırasında bir hata oluştu.");
            }
            return ApiResponseDTO.Failed("Kullanıcı oluşturma sırasında hata oluştu. Sonra tekrar deneyiniz.");
        }

        public async Task<ApiResponseDTO> RegisterExternalService(AuthDTO model)
        {
            List<string> roles = new() { "User" };
            var user = await _userManager.FindByEmailAsync(model.EMail);
            string token = string.Empty;
            if (user == null)
            {
                user = _mapper.Map<TodoUser>(model);
                var createResult = await _userManager.CreateAsync(user);
                if (createResult.Succeeded)
                {
                    var checkRoles = await CheckRoles(roles);
                    if (checkRoles)
                    {
                        var addRoleResult = await _userManager.AddToRolesAsync(user, roles);
                        if (!addRoleResult.Succeeded)
                        {
                            return ApiResponseDTO.Failed("Kullanıcı oluşturma sırasında hata oluştu.");
                        }
                        token = await ServiceHelper.JwtTokenProvider(user, UserRoles(user).Result);
                        return ApiResponseDTO.Success(token, "Kullanıcı başarıyla oluşturuldu. Giriş yapılıyor.");
                    }
                    return ApiResponseDTO.Failed("Rol atama sırasında bir hata oluştu.");
                }
                return ApiResponseDTO.Failed("Kullanıcı oluşturma sırasında bir hata oluştu.");
            }
            token = await ServiceHelper.JwtTokenProvider(user, UserRoles(user).Result);
            return ApiResponseDTO.Success(token, "Giriş başarılı");
        }

        public async Task<ApiResponseDTO> UserProfile(string userId)
        {
            var result = await _userManager.FindByIdAsync(userId);
            return ApiResponseDTO.Success(result, "Kullanıcı Profili");
        }


        #region Helper
        private async Task<List<Claim>> UserRoles(TodoUser user)
        {
            var claims = new List<Claim>();
            var rolesList = await _userManager.GetRolesAsync(user);
            foreach (var role in rolesList)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private async Task<bool> CheckRoles(List<string> Roles)
        {
            if (Roles.Any())
            {
                foreach (var role in Roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new TodoRole() { Name = role });
                    }
                }
                return true;
            }
            return false;
        }


        #endregion
    }
}
