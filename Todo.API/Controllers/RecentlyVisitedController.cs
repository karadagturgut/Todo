using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Todo.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]/[action]")]
    [ApiController]
    public class RecentlyVisitedController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var cookieValue = Request.Cookies["RecentlyVisited"];

            if (string.IsNullOrEmpty(cookieValue))
            {
                return Ok(new List<string>() { "Daha önce bir yeri ziyaret etmediniz." });
            }

            var pathList = JsonSerializer.Deserialize<List<string>>(cookieValue);
            return Ok(pathList);
        }
    }
}
