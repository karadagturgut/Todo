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
        private readonly IUserBoardService _userBoardService;

        public BoardController(IBoardService boardService, IUserBoardService userBoardService)
        {
            _service = boardService;
            _userBoardService = userBoardService;
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
        public IActionResult GetListedBoards()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var boards = _service.GetListedBoards(new() { UserId = Convert.ToInt32(userId) });
            return Ok(boards);
        }
    }
}
