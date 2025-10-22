using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.DTO.OrganizationParticipation
{
    public class OrganizationParticipationRequestDTO
    {
    }
    public class JoinOrganizationRequestDTO
    {
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
    }
    public class DecideUserJoinStatusDTO
    {
        public int ParticipationRequestId { get; set; }
        public bool Status { get; set; }
    }
}
