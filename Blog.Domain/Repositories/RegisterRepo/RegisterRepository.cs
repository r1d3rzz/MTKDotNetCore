using Blog.Database.Models;
using Blog.Domain.RequestModels;
using Blog.Domain.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Repositories.RegisterRepo
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly BlogContext _db;

        public RegisterRepository(BlogContext db)
        {
            _db = db;
        }

        public async Task<RegisterResponse> UserRegister(RegisterRequest registerRequest)
        {
            TblUser newUser = new TblUser();
            newUser.Username = registerRequest.Username;
            newUser.Email = registerRequest.Email;
            newUser.Password = registerRequest.Password;
            await _db.TblUsers.AddAsync(newUser);
            await _db.SaveChangesAsync();

            return new RegisterResponse
            {
                Username = newUser.Username,
                Email = newUser.Email
            };
        }
    }
}
