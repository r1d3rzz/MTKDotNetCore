using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.ConsoleApp.DI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppContext = MTKDotNetCore.ConsoleApp.DI.Models.AppContext;

namespace MTKDotNetCore.ConsoleApp.DI.BlogRepo
{
    internal class BlogRepository : IBlogService
    {
        private readonly Models.AppContext context;

        public BlogRepository()
        {
            this.context = new AppContext();
        }

        public async Task<List<Blog>> GetBlogs()
        {
            return await context.Blogs.ToListAsync();
        }

        public async Task<Blog> GetBlogs(int id)
        {
            return await context.Blogs.Where(b => b.BlogId == id).FirstOrDefaultAsync();
        }
    }
}
