using BlogWebAPI.Models;
using BlogWebAPI.RequestModel;
using Microsoft.EntityFrameworkCore;

namespace BlogWebAPI.Repositories.BlogRepo
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DotNetTrainingBatch5Context _db;

        public BlogRepository(DotNetTrainingBatch5Context db)
        {
            _db = db;
        }

        public async Task<List<Blog>> GetBlogs()
        {
            return await _db.Blogs
                            .AsNoTracking()
                            .Where(b => !b.DeleteFlag)
                            .ToListAsync();
        }

        public async Task<Blog> GetBlog(int blogId)
        {
            return await _db.Blogs
                            .AsNoTracking()
                            .Where(x => x.BlogId == blogId)
                            .FirstOrDefaultAsync();
        }

        public async Task<Blog> CreateBlog(BlogRequest blog)
        {
            var newBlog = new Blog
            {
                BlogTitle = blog.BlogTitle,
                BlogAuthor = blog.BlogAuthor,
                BlogContent = blog.BlogContent,
                DeleteFlag = false
            };

            await _db.Blogs.AddAsync(newBlog);
            await _db.SaveChangesAsync();
            return newBlog;
        }

        public async Task<Blog?> EditBlog(BlogRequest blog)
        {
            var currentBlog = await _db.Blogs.FirstOrDefaultAsync(x => x.BlogId == blog.BlogId);
            if (currentBlog == null) return null;

            currentBlog.BlogTitle = blog.BlogTitle;
            currentBlog.BlogAuthor = blog.BlogAuthor;
            currentBlog.BlogContent = blog.BlogContent;
            _db.Blogs.Update(currentBlog);
            await _db.SaveChangesAsync();
            return currentBlog;
        }

        public async Task<Blog?> DeleteBlog(int blogId)
        {
            var blog = await _db.Blogs.FirstOrDefaultAsync(x => x.BlogId == blogId && !x.DeleteFlag);
            if (blog == null) return null;

            blog.DeleteFlag = true;
            await _db.SaveChangesAsync();
            return blog;
        }
    }

}
