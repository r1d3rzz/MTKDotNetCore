using BlogWebAPI.Database.Models;
using BlogWebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.XPath;
namespace BlogWebAPI.Domain.Features.Blog
{
    public class BlogService
    {
        BlogDbContext _context;

        public BlogService()
        {
            _context = new BlogDbContext();
        }

        public async Task<BlogResponseModel> GetBlogs()
        {
            BlogResponseModel model = new BlogResponseModel();

            try
            {
                var blogs = await _context.Blogs.Where(b => !b.DeleteFlag).ToListAsync();
                if (blogs != null && blogs.Count > 0)
                {
                    model.Response = BaseResponseModel.Success("200", "Blogs fetched successfully");
                }
                else
                {
                    throw new Exception("No blogs found");
                }
            }
            catch (Exception ex)
            {
                model.Response = BaseResponseModel.SystemError("500", "An error occurred while fetching blogs: " + ex.Message);
                throw;
            }

            return model;
        }

        public async Task<Result<BlogResultModel>> GetBlog(int blogId)
        {
            Result<BlogResultModel> model = new Result<BlogResultModel>();
            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.BlogId == blogId);
            try
            {
                if (blog != null)
                {
                    model = Result<BlogResultModel>.Success(new BlogResultModel
                    {
                        Blog = blog
                    },
                    "Blog fetched successfully");
                    goto End;
                }

                model = Result<BlogResultModel>.Failure("Blog not found");
            }
            catch (Exception ex)
            {
                model = Result<BlogResultModel>.SystemError("An error occurred while fetching the blog: " + ex.Message);
            }

            End:
            return model;
        }

        public async Task<Result<BlogResultModel>> AddBlog(Database.Models.Blog blog)
        {
            Result<BlogResultModel> model = new Result<BlogResultModel>();
            try
            {
                await _context.Blogs.AddAsync(blog);
                await _context.SaveChangesAsync();
                model = Result<BlogResultModel>.Success(new BlogResultModel { Blog = blog }, "Blog created successfully");
            }
            catch (Exception ex)
            {
                model = Result<BlogResultModel>.SystemError("An error occurred while creating the blog: " + ex.Message);
            }

            return model;

        }

        public async Task<Result<BlogResultModel>> UpdateBlog(int id, Database.Models.Blog blog)
        {
            Result<BlogResultModel> model = new Result<BlogResultModel>();

            try
            {
                var existingBlog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(b => b.BlogId == id);
                if (existingBlog != null)
                {
                    existingBlog.BlogTitle = blog.BlogTitle;
                    existingBlog.BlogAuthor = blog.BlogAuthor;
                    existingBlog.BlogContent = blog.BlogContent;
                    existingBlog.DeleteFlag = blog.DeleteFlag;

                    _context.Blogs.Update(existingBlog);
                    _context.Entry(existingBlog).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    model = Result<BlogResultModel>.Success(new BlogResultModel { Blog = existingBlog }, "Blog updated successfully");
                }
                else
                {
                    model = Result<BlogResultModel>.Failure("Blog not found");
                }

            }
            catch (Exception ex)
            {
                model = Result<BlogResultModel>.SystemError("An error occurred while updating the blog: " + ex.Message);
            }

            return model;
        }

        public async Task<Result<BlogResultModel>> DeleteBlog(int id)
        {
            Result<BlogResultModel> model = new Result<BlogResultModel>();

            try
            {
                var existingBlog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(b => b.BlogId == id);
                if (existingBlog != null)
                {
                    existingBlog.DeleteFlag = true;
                    _context.Blogs.Update(existingBlog);
                    _context.Entry(existingBlog).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    model = Result<BlogResultModel>.Success(new BlogResultModel { Blog = existingBlog }, "Blog deleted successfully");
                }
            }
            catch (Exception ex)
            {
                model = Result<BlogResultModel>.SystemError("An error occurred while deleting the blog: " + ex.Message);
            }

            return model;
        }
    }
}