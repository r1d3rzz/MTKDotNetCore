using System.ComponentModel.DataAnnotations;

namespace BlogWebAPI.RequestModel
{
    public class BlogRequest
    {
        public int BlogId { get; set; }

        [Required]
        public string BlogTitle { get; set; } = null!;

        [Required]
        public string BlogAuthor { get; set; } = null!;

        [Required]
        public string BlogContent { get; set; } = null!;

        public bool DeleteFlag { get; set; } = false;
    }
}
