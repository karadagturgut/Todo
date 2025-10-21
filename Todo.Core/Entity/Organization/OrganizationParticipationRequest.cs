using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Entity.Base;

namespace Todo.Core.Entity
{
    public class OrganizationParticipationRequest : MultiTenantEntity
    {
        public int UserId { get; set; }
        public virtual TodoUser User { get; set; }
    }
}
