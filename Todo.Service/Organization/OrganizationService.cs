using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core;
using Todo.Core.DTO;
using Todo.Core.Entity;

namespace Todo.Service
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IGenericRepository<Organization> _repository;

        public OrganizationService(IGenericRepository<Organization> repository)
        {
            _repository = repository;
        }

        public ApiResponseDTO AddOrganization(OrganizationDTO model)
        {
            throw new NotImplementedException();
        }

        public ApiResponseDTO AllOrganizations()
        {
            throw new NotImplementedException();
        }

        public ApiResponseDTO DeleteOrganization(DeleteOrganizationDTO model)
        {
            throw new NotImplementedException();
        }

        public ApiResponseDTO UpdateOrganization(UpdateOrganizationDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
