using Blog.Database.Models;
using Blog.Domain.ResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Repositories.BlogRepo;

public interface IBlogRepository
{
    Task<List<TblBlog>> GetBlogs();

    Task<List<TblBlog>> GetRandomBlogs(int TotalBlogs);

    Task<TblBlog> GetBlogById(int BlogId);

    Task<List<TblBlog>> GetBlogsByCategoryId(int CategoryId);

    Task<PaginatedBlogsResult> GetBlogsByPagination(int PageNumber, int PageSize);

    Task StoreBlog(TblBlog Blog);

    Task UpdateBlog(TblBlog Blog);

    Task DestoryBlog(int BlogId);
}
