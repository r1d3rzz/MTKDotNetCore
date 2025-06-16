using BlogRepoAPI.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogRepoAPI.Domain.Models
{
    public class BlogResultModel
    {
        public Blog Blog { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
