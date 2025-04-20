using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Core;
using Todo.Core.Entity;
using Todo.Web.Models.Board;
using Todo.Web.Models.Organization;

namespace Todo.Web.Controllers.Board
{
    [AllowAnonymous]
    public class BoardController : Controller
    {
        private readonly IBoardService _service;

        public BoardController(IBoardService service)
        {
            _service = service;
        }

        [HttpGet]
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
            CreateBoardDTO dto = new() { Name = model.Name, OrganizationId = model.OrganizationId };
            var addService = _service.Add(dto);
            if (!addService.IsSuccess)
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var board = _service.GetBoard(new() { BoardId = id });

            if (!board.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            var boardData = Convert.ChangeType(board.Data, typeof(Todo.Core.Board)) as Todo.Core.Board;

            AddBoardViewModel viewModel = new()
            { OrganizationId = boardData.OrganizationId, Name = boardData.Name };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddBoardViewModel model)
        {
            UpdateBoardDTO dto = new() { Name = model.Name};
            var addService = _service.Update(dto);
            if (!addService.IsSuccess)
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #region json result

        public JsonResult ActiveBoards()
        {
            var boardsList = _service.GetActiveBoards();
            return Json(boardsList);
        }

        #endregion
    }
}
