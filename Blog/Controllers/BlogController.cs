using Blog.Database.Models;
using Blog.Domain.ResultModels;
using Blog.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService _blogService)
        {
            this._blogService = _blogService;
        }

        public async Task<IActionResult> Index(int? CategoryId, string Search = "")
        {
            List<TblBlog> blogs;

            if (CategoryId.HasValue && CategoryId.Value != 0)
            {
                blogs = await _blogService.GetBlogsByCategoryId(CategoryId.Value);
            }
            else
            {
                blogs = await _blogService.GetBlogs();
            }

            if (!string.IsNullOrWhiteSpace(Search))
            {
                blogs = blogs
                    .Where(x => x.Title.Contains(Search, StringComparison.OrdinalIgnoreCase)
                             || x.Detail.Contains(Search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.TotalBlogsCount = blogs.Count();
            blogs = blogs.Take(6).ToList();

            return View(blogs);
        }


        public async Task<IActionResult> GetBlogsByPagination(int pageNumber = 1, int pageSize = 6)
        {
            PaginatedBlogsResult paginatedBlogsResult;
            paginatedBlogsResult = await _blogService.GetBlogsByPagination(pageNumber, pageSize);
            ViewBag.TotalBlogsCount = paginatedBlogsResult.TotalCount;
            return View("Index", paginatedBlogsResult.Blogs);
        }

        public async Task<IActionResult> Show(int BlogId)
        {
            var blogs = await _blogService.GetBlogById(BlogId);
            ViewBag.RandomBlogs = await _blogService.GetRandomBlogs(6);
            return View(blogs);
        }
    }
}
