using Blog.Domain.Repositories.LoginRepo;
using Blog.Domain.RequestModels;
using Blog.Domain.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Services;

public class LoginService
{
    private readonly ILoginRepository _login;

    public LoginService(ILoginRepository login)
    {
        _login = login;
    }

    public async Task<LoginResponse> UserLogin(LoginRequest loginRequest)
    {
        return await _login.UserLogin(loginRequest); 
    }

    public async Task StoreUserLogin(LoginResponse loginResponse)
    {
        await _login.StoreUserLogin(loginResponse);
    }
}
