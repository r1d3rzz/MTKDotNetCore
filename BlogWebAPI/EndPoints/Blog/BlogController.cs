using BlogWebAPI.Domain.Features.Blog;
using BlogWebAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogWebAPI.EndPoints.Blog
{
    [Route("api/")]
    [ApiController]
    public class BlogController : BaseController
    {
        BlogService _blogService;

        public BlogController()
        {
            _blogService = new BlogService();
        }

        [HttpGet("blogs")]
        public async Task<IActionResult> GetBlogs()
        {
            var model = await _blogService.GetBlogs();

            return Execute(model);
        }

        [HttpGet("blogs/{id}")]
        public async Task<IActionResult> GetBlog(int id)
        {
            var model = await _blogService.GetBlog(id);
            return Execute(model);
        }

        [HttpPost("blogs")]
        public async Task<IActionResult> AddBlog(Database.Models.Blog blog)
        {
            var model = await _blogService.AddBlog(blog);
            return Execute(model);
        }

        [HttpPut("blogs/{id}")]
        public async Task<IActionResult> UpdateBlog(int id, Database.Models.Blog blog)
        {
            var model = await _blogService.UpdateBlog(id, blog);
            return Execute(model);
        }

        [HttpDelete("blogs/{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var model = await _blogService.DeleteBlog(id);
            return Execute(model);
        }
    }
}
