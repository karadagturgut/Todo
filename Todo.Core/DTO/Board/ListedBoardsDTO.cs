using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public class ListedBoardsDTO
    {
        public int UserId { get; set; }
    }
    public class OrganizationBoardsDTO
    {
        public int OrganizationId { get; set; }

        public OrganizationBoardsDTO(int organizationId)
        {
            OrganizationId = organizationId;
        }
    }
}
