using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Core.DTO;
using Todo.Core.DTO.OrganizationParticipation;
using Todo.Core.Interface.Service.OrganizationParticipation;

namespace Todo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OrganizationParticipationController : BaseController
    {
        private readonly IOrganizationParticipationService _service;

        public OrganizationParticipationController(IOrganizationParticipationService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult OrganizationJoinRequest(JoinOrganizationRequestDTO model)
        {
            var result = _service.OrganizationJoinRequest(model);
            return ApiResponse(result);
        }
        [HttpPost]
        public IActionResult DecideUserJoinStatus(DecideUserJoinStatusDTO model)
        {
            var result = _service.DecideUserJoinStatus(model);
            return ApiResponse(result);
        }
    }
}
