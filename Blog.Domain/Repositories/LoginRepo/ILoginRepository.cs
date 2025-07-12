using Blog.Domain.RequestModels;
using Blog.Domain.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Repositories.LoginRepo
{
    public interface ILoginRepository
    {
        Task<LoginResponse> UserLogin(LoginRequest loginRequest);

        Task StoreUserLogin(LoginResponse loginResponse);
    }
}
