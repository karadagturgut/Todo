using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Web.Controllers.Error
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View("404");
        }

        [Route("Error/500")]
        public IActionResult Server()
        {
            Response.StatusCode = 500;
            return View("500");
        }

        [Route("Error/{statusCode}")]
        public IActionResult Handle(int statusCode)
        {
            if (statusCode == 404)
                return RedirectToAction("PageNotFound");

            if (statusCode == 500)
                return RedirectToAction("Server");

            return View("GenericError"); // alternatif fallback
        }
    }
}
