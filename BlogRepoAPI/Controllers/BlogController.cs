using BlogRepoAPI.Database.Models;
using BlogRepoAPI.Domain.BlogRepository;
using BlogRepoAPI.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogRepoAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BlogController : BaseController
    {
        private readonly IBlogRepository _blogRepository;
        private readonly BlogService _blogServie;

        public BlogController()
        {
            _blogRepository = new BlogRepository();
            _blogServie = new BlogService(_blogRepository);
        }

        [HttpGet("/blogs")]
        public async Task<IActionResult> Index()
        {
            var model = await _blogServie.GetBlogs();
            return Execute(model);
        }

        [HttpGet("/blogs/{id:int}")]
        public async Task<IActionResult> Show(int id)
        {
            var model = await _blogServie.GetBlog(id);
            return Execute(model);
        }

        [HttpPost("/blogs")]
        public async Task<IActionResult> Store(Blog blog)
        {
            var model = await _blogServie.CreateBlog(blog);
            return Execute(model);
        }

        [HttpPut("/blogs")]
        public async Task<IActionResult> Update(Blog blog)
        {
            var model = await _blogServie.UpdateBlog(blog);
            return Execute(model);
        }

        [HttpDelete("/blogs/{id:int}")]
        public async Task<IActionResult> Destroy(int id)
        {
            var model = await _blogServie.DeleteBlog(id);
            return Execute(model);
        }
    }
}
