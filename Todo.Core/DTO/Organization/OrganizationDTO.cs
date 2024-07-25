using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.DTO
{
    public class OrganizationDTO
    {
        public string Name { get; set; }
    }
    public class UpdateOrganizationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DeleteOrganizationDTO
    {
        public int Id { get; set; }
    }
}
