using Microsoft.AspNetCore.Mvc;
using Todo.Core;

namespace Todo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _service;

        public BoardController(IBoardService boardService)
        {
            _service = boardService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult ActiveBoards()
        {
            var result = _service.GetActiveBoards();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(CreateBoardDTO model)
        {
            var result = _service.Add(model);
            return Ok(result);
        }

        [HttpPatch]
        public IActionResult Update(UpdateBoardDTO model)
        {
            var result = _service.Update(model);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(DeleteBoardDTO model)
        {
            var result = _service.Delete(model);
            return Ok(result);
        }
    }
}
