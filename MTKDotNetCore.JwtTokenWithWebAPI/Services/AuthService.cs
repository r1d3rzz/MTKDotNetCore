using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MTKDotNetCore.JwtTokenWithWebAPI.Models;
using MTKDotNetCore.JwtTokenWithWebAPI.RequestModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MTKDotNetCore.JwtTokenWithWebAPI.Services;

public class AuthService : IAuthService
{
    protected readonly IConfiguration _configuration;
    protected readonly TestContext _context;

    public AuthService(IConfiguration configuration, TestContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<string?> LoginAsync(UserRequestModel request)
    {
        var user = await _context.Users.FirstAsync(x => x.Name == request.Name);

        if (user == null) return null;

        if (new PasswordHasher<UserRequestModel>().VerifyHashedPassword(request, user.Password, request.Password) == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return CreateToken(user);
    }

    public async Task<User?> RegisterAsync(UserRequestModel request)
    {
        if (await _context.Users.AnyAsync(x => x.Name == request.Name)) return null;

        var hashedPassword = new PasswordHasher<UserRequestModel>().HashPassword(request, request.Password);

        User newUser = new User();
        newUser.Name = request.Name;
        newUser.Password = hashedPassword;
        newUser.Role = "User";
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role!),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSetup:token")!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("JwtSetup:issuer")!,    
            audience: _configuration.GetValue<string>("JwtSetup:audience")!,    
            signingCredentials: creds,
            expires: DateTime.Now.AddDays(1),
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
