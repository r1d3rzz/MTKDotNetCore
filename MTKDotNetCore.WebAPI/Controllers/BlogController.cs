using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.ConsoleApp.Model;
using System.Reflection.Metadata;

namespace MTKDotNetCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly DotNetTrainingBatch5Context context = new DotNetTrainingBatch5Context();

        [HttpGet]
        public IActionResult Index()
        {
            List<Blog> blogs = context.Blogs.Where(b => b.DeleteFlag != true).ToList();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult Show(int id)
        {
            Blog blog = context.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
            {
                return NotFound();
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            Blog newBlog = new Blog();
            newBlog.BlogTitle = blog.BlogTitle;
            newBlog.BlogAuthor = blog.BlogAuthor;
            newBlog.BlogContent = blog.BlogContent;
            context.Blogs.Add(newBlog);
            context.SaveChanges();
            return Ok(newBlog);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Blog blog)
        {
            Blog currentBlog = context.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (currentBlog is null)
            {
                return NotFound();
            }

            currentBlog.BlogTitle = blog.BlogTitle;
            currentBlog.BlogAuthor = blog.BlogAuthor;
            currentBlog.BlogContent = blog.BlogContent;

            context.Blogs.Update(currentBlog);
            context.Entry(currentBlog).State = EntityState.Modified;
            context.SaveChanges();

            return Ok(currentBlog);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUpdate(int id, Blog blog)
        {
            Blog currentBlog = context.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (currentBlog is null)
            {
                return NotFound();
            }

            if (!String.IsNullOrEmpty(blog.BlogTitle))
            {
                currentBlog.BlogTitle = blog.BlogTitle;
            }

            if (!String.IsNullOrEmpty(blog.BlogAuthor))
            {
                currentBlog.BlogAuthor = blog.BlogAuthor;
            }

            if (!String.IsNullOrEmpty(blog.BlogContent))
            {
                currentBlog.BlogContent = blog.BlogContent;
            }

            if (blog.DeleteFlag != true)
            {
                currentBlog.DeleteFlag = false;
            }

            context.Blogs.Update(currentBlog);
            context.Entry(currentBlog).State = EntityState.Modified;
            context.SaveChanges();

            return Ok(currentBlog);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Blog currentBlog = context.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (currentBlog is null)
            {
                return NotFound();
            }

            currentBlog.DeleteFlag = true;
            context.Blogs.Update(currentBlog);
            context.Entry(currentBlog).State = EntityState.Modified;
            context.SaveChanges();

            return Ok(currentBlog);
        }
    }
}
