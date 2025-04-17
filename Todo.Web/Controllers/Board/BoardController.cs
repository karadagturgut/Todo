using Microsoft.AspNetCore.Mvc;
using Todo.Core;
using Todo.Web.Models.Board;

namespace Todo.Web.Controllers.Board
{
    public class BoardController : Controller
    {
        private readonly IBoardService _service;

        public BoardController(IBoardService service)
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
        public IActionResult Add(AddBoardViewModel model)
        {
            CreateBoardDTO dto = new CreateBoardDTO { Name = model.Name, OrganizationId = 0  };
            var addService = _service.Add(dto);
            if (!addService.IsSuccess)
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
    }
}
