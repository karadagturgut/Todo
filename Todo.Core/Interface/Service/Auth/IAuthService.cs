using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.DTO.Auth;

namespace Todo.Core
{
    public interface IAuthService
    {
        ApiResponseDTO Authorize(AuthorizeDTO model);
        IEnumerable<ActionRoles> GetActionRolesByPath(string path);
        Task<ApiResponseDTO> Login(LoginDTO model);
        Task<ApiResponseDTO> Register(AuthDTO model);
        Task<ApiResponseDTO> ChangePassword(ChangePasswordDTO model);
        Task<ApiResponseDTO> RegisterExternalService(AuthDTO model);
        Task<ApiResponseDTO> UserProfile(string userId);
        Task<ApiResponseDTO> AddRole(RoleDTO model);
        ApiResponseDTO GetAllRoles();

    }
}
