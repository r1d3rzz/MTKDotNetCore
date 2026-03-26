using MTKDotNetCore.JwtTokenWithWebAPI.Models;
using MTKDotNetCore.JwtTokenWithWebAPI.RequestModels;

namespace MTKDotNetCore.JwtTokenWithWebAPI.Services;

public interface IAuthService
{
    Task<string?> LoginAsync(UserRequestModel request);
    Task<User?> RegisterAsync(UserRequestModel request);
}
