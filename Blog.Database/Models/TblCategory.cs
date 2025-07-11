using System;
using System.Collections.Generic;

namespace Blog.Database.Models;

public partial class TblCategory
{
    public int TblCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public bool DeleteFlag { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ICollection<TblBlog>? TblBlogs { get; set; }

}
