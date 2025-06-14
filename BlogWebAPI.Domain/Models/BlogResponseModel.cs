using BlogWebAPI.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebAPI.Domain.Models
{
    public class BlogResponseModel
    {
        public BaseResponseModel Response { get; set; }
    }

    public class BlogResultModel
    {
        public Blog Blog { get; set; }
    }
}
