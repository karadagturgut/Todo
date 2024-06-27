using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public interface IAuthService
    {
        Task<ApiResponseDTO> Login(LoginDTO model);
        Task<ApiResponseDTO> Register(AuthDTO model);
        Task<ApiResponseDTO> ChangePassword(ChangePasswordDTO model);
        ApiResponseDTO Authorize();
        Task<ApiResponseDTO> RegisterExternalService(AuthDTO model);
    }
}
