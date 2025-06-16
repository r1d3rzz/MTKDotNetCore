using BlogRepoAPI.Database.Models;
using BlogRepoAPI.Domain.BlogRepository;
using BlogRepoAPI.Domain.Models;

namespace BlogRepoAPI.Domain.Services
{
    public class BlogService
    {
        private readonly IBlogRepository blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }

        public async Task<Result<BlogResultModel>> GetBlog(int blogId)
        {
            Result<BlogResultModel> model = new Result<BlogResultModel>();

            model = Result<BlogResultModel>.Success(new BlogResultModel { Blog = await blogRepository.GetBlogByIdAsync(blogId) }, "Fetch Blog Success");

            return model;
        }

        public async Task<Result<BlogResultModel>> GetBlogs()
        {
            Result<Models.BlogResultModel> model = new Result<BlogResultModel>();

            model = Result<BlogResultModel>.Success(new BlogResultModel { Blogs = await blogRepository.GetBlogsAsync() }, "Blog Fetch Success");

            return model;
        }

        public async Task<Result<BlogResultModel>> CreateBlog(Blog blog)
        {
            Result<BlogResultModel> model = new Result<BlogResultModel>();

            try
            {
                await blogRepository.AddBlogAsync(blog);

                model = Result<BlogResultModel>.Success(new BlogResultModel { Blog = blog }, "Blog Create Success");
            }
            catch (Exception ex)
            {
                model = Result<BlogResultModel>.ValidationError("Fail to create blog");
            }

            return model;
        }

        public async Task<Result<BlogResultModel>> UpdateBlog(Blog blog)
        {
            Result<BlogResultModel> model = new Result<BlogResultModel>();

            try
            {
                var existBlog = await blogRepository.GetBlogByIdAsync(blog.BlogId);
                if (existBlog != null)
                {
                    await blogRepository.UpdateBlogAsync(blog);
                    model = Result<BlogResultModel>.Success(new BlogResultModel { Blog = existBlog }, "Blog Update Success");
                }
            }
            catch (Exception ex)
            {
                model = Result<BlogResultModel>.ValidationError("Fail to update blog");
            }

            return model;
        }

        public async Task<Result<BlogResultModel>> DeleteBlog(int blogId)
        {
            Result<BlogResultModel> model = new Result<BlogResultModel>();

            try
            {
                var blog = await blogRepository.GetBlogByIdAsync(blogId);

                if (blog != null)
                {
                    await blogRepository.DeleteBlogAsync(blogId);
                    model = Result<BlogResultModel>.Success(new BlogResultModel { Blog = blog }, "Blog Delete Success");
                }

            }
            catch (Exception ex)
            {
                model = Result<BlogResultModel>.ValidationError("Fail to Delete blog");
            }

            return model;
        }
    }
}
