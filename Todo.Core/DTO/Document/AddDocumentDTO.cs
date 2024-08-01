using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core
{
    public class AddDocumentDTO
    {
        public IFormFile File { get; set; }
    }
}
