using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.ResponseModels
{
    public class LoginResponse
    {
        public int UserId { get; set; }

        public string SessionId { get; set; } = null!;

        public DateTime SessionExpired { get; set; } = DateTime.Now.AddMinutes(5);
    }
}
