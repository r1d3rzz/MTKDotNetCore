using BlogRepoAPI.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogRepoAPI.Domain.BlogRepository
{
    public interface IBlogRepository
    {
        Task<Blog> GetBlogByIdAsync(int blogId);
        Task<List<Blog>> GetBlogsAsync();
        Task AddBlogAsync(Blog blog);
        Task UpdateBlogAsync(Blog blog);
        Task DeleteBlogAsync(int blogId);
    }
}
