using BlogWebAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlogWebAPI.EndPoints
{
    [Route("api/")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult Execute(object model)
        {
            JObject JObj = JObject.Parse(JsonConvert.SerializeObject(model));

            if (JObj.ContainsKey("Response"))
            {
                BaseResponseModel baseResponse = JsonConvert.DeserializeObject<BaseResponseModel>(JObj["Response"]!.ToString())!;

                if (baseResponse.RespType == EnumRespType.ValidationError) return BadRequest(model);

                if (baseResponse.RespType == EnumRespType.SystemError) return StatusCode(500, model);

                return Ok(model);
            }

            return StatusCode(500, "Invalid Response Model. Please add BaseResponseModel to your response");
        }

        public IActionResult Execute<T>(Result<T> model)
        {
            if (model.Type == EnumType.ValidationError) return BadRequest(model);
            if (model.Type == EnumType.SystemError) return StatusCode(500, model);
            return Ok(model);
        }
    }
}
