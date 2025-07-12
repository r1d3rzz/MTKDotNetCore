using Blog.Database.Models;
using Blog.Domain.RequestModels;
using Blog.Domain.ResponseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Repositories.LoginRepo;

public class LoginRepository : ILoginRepository
{
    private readonly BlogContext _db;

    public LoginRepository(BlogContext db)
    {
        _db = db;
    }

    public async Task StoreUserLogin(LoginResponse loginResponse)
    {
		try
		{
			TblLogin newLogin = new TblLogin();
			newLogin.TblUserId = loginResponse.UserId;
			newLogin.SessionId = loginResponse.SessionId;
			newLogin.SessionExpired = loginResponse.SessionExpired;
			await _db.TblLogins.AddAsync(newLogin);
			await _db.SaveChangesAsync();
		}
        catch (Exception ex)
		{
			throw;
		}
    }

    public async Task<LoginResponse> UserLogin(LoginRequest loginRequest)
    {
		try
		{
			var authUser = await _db.TblUsers.FirstOrDefaultAsync(x =>
			x.Email == loginRequest.Email &&
			x.Password == loginRequest.Password);

			if (authUser == null)
			{
				throw new Exception("User Not Found!");
			}

			return new LoginResponse
			{
				UserId = authUser.TblUserId,
				SessionId = Guid.NewGuid().ToString()
			};
		}
		catch (Exception ex)
		{
			throw;
			throw;
		}
    }
}
