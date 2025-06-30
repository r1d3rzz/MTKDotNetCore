using Microsoft.AspNetCore.Mvc;
using MTKDotNetCore.AuthAPI.ActionFilters;
using MTKDotNetCore.AuthAPI.DbModels;
using MTKDotNetCore.AuthAPI.Helpers;
using MTKDotNetCore.AuthAPI.RequestModels;
using MTKDotNetCore.AuthAPI.ResponseModels;
using Newtonsoft.Json;

namespace MTKDotNetCore.AuthAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly EncDecHelper _encDecHelper;
        public AuthController(EncDecHelper encDecHelper)
        {
            _encDecHelper = encDecHelper;
        }

        [HttpPost("Login")]
        public IActionResult Login(RequestUser request)
        {
            List<User> Users = new List<User>
            {
                new User {Username = "rider", Password = "rider"},
                new User {Username = "mtk", Password = "mtk"}
            };

            User user = Users.Where(x => x.Username == request.Username).First();

            if (user == null)
                return NotFound("User Not Found");

            ResponseUser response = new ResponseUser
            {
                Username = user.Username,
                SessionId = Guid.NewGuid().ToString(),
                SessionExpired = DateTime.Now.AddMinutes(5)
            };

            var encJson = _encDecHelper.EncString(JsonConvert.SerializeObject(response));

            return Ok(new ResponseLogin
            {
                AuthToken = encJson
            });
        }

        [ServiceFilter<ValidationTokenFilter>]
        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            
            List<User> Users = new List<User>
            {
                new User {Username = "rider", Password = "rider"},
                new User {Username = "mtk", Password = "mtk"}
            };

            return Ok(Users);
        }
    }
}
