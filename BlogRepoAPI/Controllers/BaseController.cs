using BlogRepoAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogRepoAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult Execute<T>(Result<T> model)
        {
            if (model.Type == ResultType.SystemError) return BadRequest();
            if (model.Type == ResultType.ValidationError) return StatusCode(500, model);
            return Ok(model);
        }
    }
}
