using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Todo.Core;

namespace Todo.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<TodoUser> _userManager;
        private readonly RoleManager<TodoRole> _roleManager;
        private readonly IMapper _mapper;
        ServiceHelper helper = new ServiceHelper();
        public AuthService(UserManager<TodoUser> userManager, RoleManager<TodoRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public ApiResponseDTO Authorize()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponseDTO> ChangePassword(AuthDTO model)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponseDTO> Login(AuthDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = helper.JwtTokenProvider(user, UserRoles(user).Result);
                return ApiResponseDTO.Success(token, null);
            }
            return ApiResponseDTO.Failed("Kullanıcı adı ya da şifre hatalı.");
        }

        public async Task<ApiResponseDTO> Register(AuthDTO model)
        {
            var user = _mapper.Map<TodoUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var checkRoles = await CheckRoles(new() { "Admin" });
                if (checkRoles)
                {
                    await _userManager.AddToRolesAsync(user, new List<string>() { "Admin" });
                    return ApiResponseDTO.Success(user.Id, "Kullanıcı başarıyla oluşturuldu.");
                }
                return ApiResponseDTO.Failed("Rol atama sırasında bir hata oluştu.");
            }
            return ApiResponseDTO.Failed("Kullanıcı oluşturma sırasında hata oluştu. Sonra tekrar deneyiniz.");
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
