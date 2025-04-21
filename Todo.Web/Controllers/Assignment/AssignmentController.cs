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
            // 1. sırasıyla organization board servislerinden seçim yapılacak
            // 2. sonrasında buradaki liste dolacak.
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

        #region
        public JsonResult AssignmentList(int? boardId)
        {
            var assignments = _service.FilterByBoardId(new() { BoardId = boardId });
            return Json(assignments);
        }
        #endregion
    }
}
