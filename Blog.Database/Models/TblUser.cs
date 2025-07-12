using System;
using System.Collections.Generic;

namespace Blog.Database.Models;

public partial class TblUser
{
    public int TblUserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Avatar { get; set; }

    public bool DeleteFlag { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public ICollection<TblBlog>? TblBlogs { get; set; }
}
