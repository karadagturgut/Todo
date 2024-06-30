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
        [Authorize(Roles = "Admin")]
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

        [HttpPost]
        public IActionResult GetListedBoards(ListedBoardsDTO model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var boardIdList = _userBoardService.BoardsByUserId(new() { UserId = Convert.ToInt32(userId) });
            var boards = _service.GetListedBoards(new ListedBoardsDTO() { BoardList = (List<int>)boardIdList.Data ??new() { 0 } });
            return Ok(boards);
        }
    }
}
