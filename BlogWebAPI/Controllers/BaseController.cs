using BlogWebAPI.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult Execute<T>(Result<T> model)
        {
            if (model.Type == EnumType.ValidationError)
                return BadRequest(model);

            if (model.Type == EnumType.SystemError)
                return BadRequest(model);

            return Ok(model);
        }
    }
}
