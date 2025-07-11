using Blog.Database.Models;
using Blog.Domain.ResultModels;
using Microsoft.EntityFrameworkCore;

namespace Blog.Domain.Repositories.BlogRepo
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _blogContext;

        public BlogRepository(BlogContext _blogContext)
        {
            this._blogContext = _blogContext;
        }

        public Task DestoryBlog(int BlogId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TblBlog>> GetBlogs()
        {
            List<TblBlog> blogs = await _blogContext.TblBlogs.Where(x => x.DeleteFlag == false).Include(x => x.TblUser).Include(x => x.TblCategory).OrderByDescending(x => x.UpdatedAt).ToListAsync();
            return blogs;
        }

        public async Task<PaginatedBlogsResult> GetBlogsByPagination(int pageNumber, int pageSize)
        {
            var query = _blogContext.TblBlogs
                .Where(x => !x.DeleteFlag)
                .Include(x => x.TblUser)
                .Include(x => x.TblCategory);

            int totalCount = await query.CountAsync();

            List<TblBlog> blogs = await query
                .OrderByDescending(x => x.UpdatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedBlogsResult
            {
                Blogs = blogs,
                TotalCount = totalCount
            };
        }

        public async Task<TblBlog> GetBlogById(int BlogId)
        {
            var blog = await _blogContext.TblBlogs.FirstOrDefaultAsync(b => b.TblBlogId == BlogId);
            return blog;
        }

        public async Task<List<TblBlog>> GetRandomBlogs(int TotalBlogs)
        {
            List<TblBlog> blogs = await _blogContext.TblBlogs.Where(x => x.DeleteFlag == false).Include(x => x.TblUser).Include(x => x.TblCategory).Take(TotalBlogs).OrderBy(x => Guid.NewGuid()).ToListAsync();
            return blogs;
        }

        public Task StoreBlog(TblBlog Blog)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBlog(TblBlog Blog)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TblBlog>> GetBlogsByCategoryId(int CategoryId)
        {
            List<TblBlog> blogs = await _blogContext.TblBlogs.Where(x => x.TblCategoryId == CategoryId && x.DeleteFlag == false).Include(x => x.TblUser).Include(x => x.TblCategory).OrderByDescending(x => x.UpdatedAt).ToListAsync();
            return blogs;
        }
    }
}
