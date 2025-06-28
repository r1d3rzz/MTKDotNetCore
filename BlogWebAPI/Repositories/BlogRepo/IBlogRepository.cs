
using BlogWebAPI.Models;
using BlogWebAPI.RequestModel;

namespace BlogWebAPI.Repositories.BlogRepo
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetBlogs();

        Task<Blog> GetBlog(int BlogId);

        Task<Blog> CreateBlog(BlogRequest blog);

        Task<Blog?> EditBlog(BlogRequest blog);

        Task<Blog?> DeleteBlog(int BlogId);
    }
}
