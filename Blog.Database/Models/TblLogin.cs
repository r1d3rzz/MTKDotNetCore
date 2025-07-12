using System;
using System.Collections.Generic;

namespace Blog.Database.Models;

public partial class TblLogin
{
    public int TblLoginId { get; set; }

    public int TblUserId { get; set; }

    public string SessionId { get; set; } = null!;

    public DateTime SessionExpired { get; set; }
}
