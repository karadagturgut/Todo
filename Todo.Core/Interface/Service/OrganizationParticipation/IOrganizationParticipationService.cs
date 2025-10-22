using Todo.Core.DTO.OrganizationParticipation;

namespace Todo.Core.Interface.Service.OrganizationParticipation
{
    public interface IOrganizationParticipationService
    {
        ApiResponseDTO OrganizationJoinRequest(JoinOrganizationRequestDTO model);
        ApiResponseDTO GetAll(JoinOrganizationRequestDTO model);
        ApiResponseDTO DecideUserJoinStatus(DecideUserJoinStatusDTO model);
    }
}
