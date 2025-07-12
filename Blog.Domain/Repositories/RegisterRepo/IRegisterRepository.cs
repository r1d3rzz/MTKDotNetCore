using Blog.Domain.RequestModels;
using Blog.Domain.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Repositories.RegisterRepo
{
    public interface IRegisterRepository
    {
        Task<RegisterResponse> UserRegister(RegisterRequest registerRequest);
    }
}
