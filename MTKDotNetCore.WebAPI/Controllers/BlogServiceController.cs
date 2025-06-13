using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.ConsoleApp.Model;
using MTKDotNetCore.Domain.Features.Blog;
using System.Reflection.Metadata;

namespace MTKDotNetCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogServiceController : ControllerBase
    {
        protected BlogService _blogService;

        public BlogServiceController()
        {
            _blogService = new BlogService();
        }
        private readonly DotNetTrainingBatch5Context context = new DotNetTrainingBatch5Context();

        [HttpGet]
        public IActionResult Index()
        {
            List<Blog> blogs = _blogService.GetBlogs();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult Show(int id)
        {
            Blog blog = _blogService.GetBlogById(id);
            if (blog is null)
            {
                return NotFound();
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            Blog newBlog = _blogService.AddBlog(blog);
            return Ok(newBlog);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Blog blog)
        {
            Blog currentBlog = _blogService.UpdateBlog(id, blog);
            return Ok(currentBlog);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _blogService.DeleteBlog(id);
            
            if (isDeleted == true)
            {
                return Ok(new { message = "Blog deleted successfully." });
            }

            return NotFound();
        }
    }
}
