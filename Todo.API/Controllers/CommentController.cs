using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Core;

namespace Todo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public IActionResult Add(CommentDTO model)
        {
            var result = _commentService.Add(model);
            return ApiResponse(result);
        }

        [HttpPatch]
        public IActionResult Edit(CommentDTO model) 
        {
            var result = _commentService.Update(model);
            return ApiResponse(result);
        }

        [HttpPost]
        public IActionResult AssignmentComments(GetCommentDTO model) 
        {
            var result = _commentService.GetByAssignmentId(model.AssignmentId);
            return ApiResponse(result);
        }
        
        [HttpDelete]
        public IActionResult Delete(DeleteCommentDTO model) 
        {
            var result = _commentService.Delete(model.Id);
            return ApiResponse(result);
        }
    }
}
