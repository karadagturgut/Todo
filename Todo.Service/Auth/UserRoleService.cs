using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core;
using Todo.Core.DTO.Auth;

namespace Todo.Service.Auth
{
    public partial class AuthService : IAuthService
    {
        public async Task<ApiResponseDTO> AddRole(RoleDTO model)
        {
            if (!await _roleManager.RoleExistsAsync(model.Name))
            {
                await _roleManager.CreateAsync(new TodoRole() { Name = model.Name});
                return ApiResponseDTO.SuccessAdded(null, "Rol ekleme işlemi tamamlandı.");
            }
            return ApiResponseDTO.Success(null, "Eklemek istediğiniz rol bulunmaktadır.");
        } 

        public ApiResponseDTO GetAllRoles()
        {
            var allRoles = _roleManager.Roles;
            return ApiResponseDTO.Success(allRoles.ToList(), "Tüm roller listesi:");
        }
    }
}
