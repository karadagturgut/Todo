using Microsoft.AspNetCore.Mvc;
using Todo.Core;
using Todo.Web.Models.Assignment;

namespace Todo.Web.Controllers.Assignment
{
    public class AssignmentController : Controller
    {
        private readonly IAssignmentService _service;

        public AssignmentController(IAssignmentService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddAssignmentViewModel model)
        {
            return View();
        }
    }
}
