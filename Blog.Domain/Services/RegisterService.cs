using Blog.Domain.Repositories.RegisterRepo;
using Blog.Domain.RequestModels;
using Blog.Domain.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Services
{
    public class RegisterService
    {
        private readonly IRegisterRepository _registerRepository;

        public RegisterService(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }

        public async Task<RegisterResponse> CreateNewUser(RegisterRequest registerRequest)
        {
            return await _registerRepository.UserRegister(registerRequest);
        }
    }
}
