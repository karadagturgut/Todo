using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Core;

namespace Todo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BoardController : BaseController
    {
        private readonly IBoardService _service;

        public BoardController(IBoardService boardService)
        {
            _service = boardService;
        }

        [HttpGet]
        public IActionResult ActiveBoards()
        {
            var result = _service.GetActiveBoards();
            return ApiResponse(result);
        }

        [HttpPost]
        public IActionResult Add(CreateBoardDTO model)
        {
            var result = _service.Add(model);
            return ApiResponse(result);
        }

        [HttpPatch]
        public IActionResult Update(UpdateBoardDTO model)
        {
            var result = _service.Update(model);
            return ApiResponse(result);
        }

        [HttpDelete]
        public IActionResult Delete(DeleteBoardDTO model)
        {
            var result = _service.Delete(model);
            return ApiResponse(result);
        }

        [HttpGet]
        public IActionResult GetUserBoards()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var boards = _service.GetUserBoards(new() { UserId = Convert.ToInt32(userId) });
            return ApiResponse(boards);
        }

        [HttpGet]
        public IActionResult GetOrganizationBoards()
        {
            var request = new OrganizationBoardsDTO(Convert.ToInt32(User.FindFirst("Organization")?.Value));
            var boards = _service.GetOrganizationBoards(request);
            return ApiResponse(boards);
        }
    }
}
