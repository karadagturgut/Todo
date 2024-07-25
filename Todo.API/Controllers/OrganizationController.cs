using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Core;
using Todo.Core.DTO;

namespace Todo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OrganizationController : BaseController
    {
        private readonly IOrganizationService _service;

        public OrganizationController(IOrganizationService organizationService)
        {
            _service = organizationService;
        }

        [HttpGet]
        public IActionResult AllOrganizations() 
        {
            var result = _service.AllOrganizations();
            return ApiResponse(result);
        }

        [HttpPost]
        public IActionResult AddOrganization(OrganizationDTO model) 
        {
            var result = _service.AddOrganization(model);
            return ApiResponse(result); 
        }

        [HttpPost]
        public IActionResult DeleteOrganization(DeleteOrganizationDTO model)
        {
            var result = _service.DeleteOrganization(model); 
            return ApiResponse(result);
        }

        [HttpPost]
        public IActionResult UpdateOrganization(UpdateOrganizationDTO model)
        {
            var result = _service.UpdateOrganization(model);
            return ApiResponse(result);
        }
    }
}
