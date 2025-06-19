namespace MTKDotNetCore.Domain.Features.Blog;

public interface IBlogService
{
    ConsoleApp.Model.Blog? AddBlog(ConsoleApp.Model.Blog blog);
    bool? DeleteBlog(int id);
    ConsoleApp.Model.Blog? GetBlogById(int id);
    List<ConsoleApp.Model.Blog> GetBlogs();
    ConsoleApp.Model.Blog? UpdateBlog(int id, ConsoleApp.Model.Blog blog);
}