using MTKDotNetCore.ConsoleApp.DI.Models;

namespace MTKDotNetCore.ConsoleApp.DI.BlogRepo
{
    internal interface IBlogService
    {
        Task<List<Blog>> GetBlogs();

        Task<Blog> GetBlogs(int id);
    }
}