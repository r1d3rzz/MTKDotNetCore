using Blog.Database.Models;
using Blog.Domain.Repositories.BlogRepo;
using Blog.Domain.ResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Services;

public class BlogService
{
    private readonly IBlogRepository _blogRepository;

    public BlogService(IBlogRepository _blogRepository)
    {
        this._blogRepository = _blogRepository;
    }

    public async Task<List<TblBlog>> GetBlogs()
    {
        return await _blogRepository.GetBlogs();
    }

    public async Task<List<TblBlog>> GetBlogsByCategoryId(int CategoryId)
    {
        return await _blogRepository.GetBlogsByCategoryId(CategoryId);
    }

    public async Task<PaginatedBlogsResult> GetBlogsByPagination(int PageNumber, int PageSize)
    {
        return await _blogRepository.GetBlogsByPagination(PageNumber, PageSize);
    }

    public async Task<TblBlog> GetBlogById(int BlogId)
    {
        var blog = await _blogRepository.GetBlogById(BlogId);
        return blog;
    }

    public async Task<List<TblBlog>> GetRandomBlogs(int TotalBlogs)
    {
        return await _blogRepository.GetRandomBlogs(TotalBlogs);
    }
}
