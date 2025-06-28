using BlogWebAPI.Models;
using BlogWebAPI.RequestModel;
using BlogWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogWebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BlogController : BaseController
    {
        protected readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("blogs")]
        public async Task<IActionResult> Index()
        {
            return Execute(await _blogService.GetBlogs());
        }

        [HttpGet("/blogs/{id:int}")]
        public async Task<IActionResult> Show(int id)
        {
            var blog = await _blogService.GetBlogById(id);
            if (blog != null)
            {
                return Execute(blog);
            }
            else
            {
                return NotFound("Blog Not Found!");
            }
        }

        [HttpPost("/blogs")]
        public async Task<IActionResult> Store(BlogRequest blogRequest)
        {
            try
            {
                var model = await _blogService.CreateBlog(blogRequest);
                return Execute(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpPut("/blogs")]
        public async Task<IActionResult> Update(BlogRequest blogRequest)
        {
            try
            {
                var model = await _blogService.EditBlog(blogRequest);
                return Execute(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpDelete("/blogs/{id:int}")]
        public async Task<IActionResult> Destroy(int id)
        {
            try
            {
                var model = await _blogService.DeleteBlog(id);
                return Execute(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
    }
}
