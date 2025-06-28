using BlogWebAPI.Models;
using BlogWebAPI.Repositories.BlogRepo;
using BlogWebAPI.RequestModel;
using BlogWebAPI.ResponseModel;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BlogWebAPI.Services
{
    public class BlogService
    {
        protected readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<Result<List<ResultBlogModel>>> GetBlogs()
        {
            try
            {
                List<Blog> blogs = await _blogRepository.GetBlogs();

                List<ResultBlogModel> resultBlogModels = blogs.Select(blog => new ResultBlogModel
                {
                    Blog = blog
                }).ToList();

                return Result<List<ResultBlogModel>>.Success(resultBlogModels, "Blogs Success");
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<Result<ResultBlogModel>> GetBlogById(int BlogId)
        {
            Result<ResultBlogModel> model = new Result<ResultBlogModel>();
            try
            {
                Blog blog = await _blogRepository.GetBlog(BlogId);
                model = Result<ResultBlogModel>.Success(new ResultBlogModel { Blog = blog }, "Get Blog Success");
                return model;
            }
            catch (Exception ex)
            {
                return Result<ResultBlogModel>.SystemError("Blog Fetch Fail. Error: " + ex.Message);
                throw;
            }
            ;
        }

        public async Task<Result<ResultBlogModel>> CreateBlog(BlogRequest blog)
        {
            Result<ResultBlogModel> model = new Result<ResultBlogModel>();

            try
            {
                Blog newBlog = await _blogRepository.CreateBlog(blog);
                model = Result<ResultBlogModel>.Success(new ResultBlogModel { Blog = newBlog }, "Blog Created Successfully");
                return model;
            }
            catch (Exception ex)
            {
                return Result<ResultBlogModel>.SystemError("Blog Create Fail. Error: " + ex.Message);
                throw;
            }
        }

        public async Task<Result<ResultBlogModel>> EditBlog(BlogRequest blog)
        {
            Result<ResultBlogModel> model = new Result<ResultBlogModel>();
            try
            {
                Blog updateBlog = await _blogRepository.EditBlog(blog);
                model = Result<ResultBlogModel>.Success(new ResultBlogModel { Blog = updateBlog }, "Blog Edit Success");
                return model;
            }
            catch (Exception ex)
            {
                return Result<ResultBlogModel>.SystemError("Blog Edit Fail. Error: " + ex.Message);
                throw;
            }
        }

        public async Task<Result<ResultBlogModel>> DeleteBlog(int BlogId)
        {
            Result<ResultBlogModel> model = new Result<ResultBlogModel>();
            try
            {
                var blog = await _blogRepository.DeleteBlog(BlogId);
                model = Result<ResultBlogModel>.Success(new ResultBlogModel { Blog = blog }, "Blog Delete Success");
                return model;
            }
            catch (Exception ex)
            {
                return Result<ResultBlogModel>.SystemError("Blog Delete Fail. Error: " + ex.Message);
                throw;
            }
        }

    }
}
