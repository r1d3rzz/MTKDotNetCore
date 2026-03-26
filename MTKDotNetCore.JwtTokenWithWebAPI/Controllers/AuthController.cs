using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTKDotNetCore.JwtTokenWithWebAPI.Models;
using MTKDotNetCore.JwtTokenWithWebAPI.RequestModels;
using MTKDotNetCore.JwtTokenWithWebAPI.Services;

namespace MTKDotNetCore.JwtTokenWithWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        protected readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserRequestModel userRequestModel)
        {
            var token = await _authService.LoginAsync(userRequestModel);

            if (token == null) return BadRequest("Your credential is wrong!");

            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserRequestModel userRequestModel)
        {
            var user = await _authService.RegisterAsync(userRequestModel);

            if (user == null) return BadRequest("New user create fail");

            return Ok(user);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<string>> Profile()
        {
            return "user profile";
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("dashboard")]
        public async Task<ActionResult<string>> Dashboard()
        {
            return "Dashboard Page";
        }
    }
}
