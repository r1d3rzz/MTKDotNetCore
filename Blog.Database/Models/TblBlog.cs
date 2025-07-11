using System;
using System.Collections.Generic;

namespace Blog.Database.Models;

public partial class TblBlog
{
    public int TblBlogId { get; set; }

    public string Title { get; set; } = null!;

    public string Detail { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public int TblUserId { get; set; }
    public TblUser? TblUser { get; set; }

    public int TblCategoryId { get; set; }
    public TblCategory? TblCategory { get; set; }

    public bool DeleteFlag { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
