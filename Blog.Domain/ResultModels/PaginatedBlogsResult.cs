using Blog.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.ResultModels;

public class PaginatedBlogsResult
{
    public List<TblBlog> Blogs { get; set; }
    public int TotalCount { get; set; }
}