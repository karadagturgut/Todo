using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.DTO;

namespace Todo.Core
{
    public interface IOrganizationService
    {
        ApiResponseDTO AllOrganizations();
        ApiResponseDTO AddOrganization(OrganizationDTO model);
        ApiResponseDTO DeleteOrganization(DeleteOrganizationDTO model);
        ApiResponseDTO UpdateOrganization(UpdateOrganizationDTO model);
        ApiResponseDTO GetOrganization(DeleteOrganizationDTO model);
    }
}
