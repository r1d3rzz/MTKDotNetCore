using Blog.Database.Models;
using Blog.Domain.RequestModels;
using Blog.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Blog.Controllers;

public class AuthController : Controller
{
    private readonly LoginService _loginService;
    private readonly RegisterService _registerService;

    public AuthController(LoginService loginService, RegisterService registerService)
    {
        _loginService = loginService;
        _registerService = registerService;
    }

    #region Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var loginResponse = await _loginService.UserLogin(loginRequest);

                if (loginRequest == null)
                {
                    throw new Exception("Email or Password is Invalid!");
                }

                await _loginService.StoreUserLogin(loginResponse);

                HttpContext.Response.Cookies.Delete("Authorization");

                CookieOptions options = new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddMinutes(10),
                    HttpOnly = true, // Accessible only by the server
                    IsEssential = true // Required for GDPR compliance
                };

                string token = loginResponse.UserId.ToString() + "|" + loginResponse.SessionExpired.ToString();

                HttpContext.Response.Cookies.Append("Authorization", token, options);

                return Redirect("/");
            }
            else
            {
                throw new Exception("Input Value is Invalid");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    public async Task<IActionResult> logout()
    {
        if (HttpContext.Request.Cookies["Authorization"] != string.Empty)
        {
            HttpContext.Response.Cookies.Delete("Authorization");
            return RedirectToAction("Login");
        }

        return Redirect("/");
    }
    #endregion

    #region Register

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var newUser = await _registerService.CreateNewUser(registerRequest);
                return RedirectToAction("Login");
            }
            else
            {
                throw new Exception("User Create Fail");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

}
