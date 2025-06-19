using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.ConsoleApp.Model;

using BlogModel = MTKDotNetCore.ConsoleApp.Model.Blog;

namespace MTKDotNetCore.Domain.Features.Blog
{
    public class BlogService : IBlogService
    {
        private readonly DotNetTrainingBatch5Context _db;

        public BlogService(DotNetTrainingBatch5Context db)
        {
            _db = db;
        }

        public List<BlogModel> GetBlogs()
        {
            return _db.Blogs.ToList();
        }

        public BlogModel? GetBlogById(int id)
        {
            BlogModel blog = _db.Blogs.First(b => b.BlogId == id);
            if (blog == null)
            {
                return null;
            }
            return blog;
        }

        public BlogModel? AddBlog(BlogModel blog)
        {
            _db.Blogs.Add(blog);
            int result = _db.SaveChanges();
            return result > 0 ? blog : null;
        }

        public BlogModel? UpdateBlog(int id, BlogModel blog)
        {
            BlogModel currentBlog = _db.Blogs.AsNoTracking().First(x => x.BlogId == id);
            if (currentBlog is null)
            {
                return null;
            }
            currentBlog.BlogTitle = blog.BlogTitle;
            currentBlog.BlogAuthor = blog.BlogAuthor;
            currentBlog.BlogContent = blog.BlogContent;
            currentBlog.DeleteFlag = blog.DeleteFlag;
            _db.Blogs.Update(currentBlog);
            _db.Entry(currentBlog).State = EntityState.Modified;
            int result = _db.SaveChanges();
            return result > 0 ? currentBlog : null;
        }

        public bool? DeleteBlog(int id)
        {
            BlogModel blog = _db.Blogs.First(b => b.BlogId == id);
            if (blog == null)
            {
                return null;
            }
            blog.DeleteFlag = true;
            _db.Blogs.Update(blog);
            _db.Entry(blog).State = EntityState.Modified;
            int result = _db.SaveChanges();
            return result > 0;
        }
    }
}
