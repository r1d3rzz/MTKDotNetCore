using MTKDotNetCore.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.ConsoleApp
{
    public class DapperExampleWithService
    {
        private readonly string _connectionString = "Data Source=DESKTOP-QREHFRH;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=rider";
        private readonly DapperService _dapperService;

        public DapperExampleWithService()
        {
            _dapperService = new DapperService(_connectionString);
        }

        public class DapperBlogModel
        {
            public int BlogId { get; set; }

            public string BlogTitle { get; set; }

            public string BlogAuthor { get; set; }

            public string BlogContent { get; set; }

            public bool DeleteFlag { get; set; }
        }

        public void Read()
        {
            string query = "SELECT * FROM Blog WHERE DeleteFlag != @deleteFlag";
            var blogs = _dapperService.Query<DapperBlogModel>(query, new DapperBlogModel { DeleteFlag = true });
            foreach (var blog in blogs)
            {
                Console.WriteLine(blog.BlogId);
                Console.WriteLine(blog.BlogTitle);
                Console.WriteLine(blog.BlogAuthor);
                Console.WriteLine(blog.BlogContent);
                Console.WriteLine(blog.DeleteFlag);
                Console.WriteLine("=====================================");
            }
        }

        public void Show()
        {
            Console.Write("Enter Blog Id: ");
            int blogId = Convert.ToInt32(Console.ReadLine());
            string query = "SELECT * FROM Blog WHERE BlogId = @blogId";
            var blogs = _dapperService.Query<DapperBlogModel>(query, new DapperBlogModel { BlogId = blogId });
            foreach (var blog in blogs)
            {
                Console.WriteLine(blog.BlogId);
                Console.WriteLine(blog.BlogTitle);
                Console.WriteLine(blog.BlogAuthor);
                Console.WriteLine(blog.BlogContent);
                Console.WriteLine(blog.DeleteFlag);
                Console.WriteLine("=====================================");
            }
        }

        public void Store()
        {
            Console.Write("Enter Blog Title: ");
            string blogTitle = Console.ReadLine();
            Console.Write("Enter Blog Author: ");
            string blogAuthor = Console.ReadLine();
            Console.Write("Enter Blog Content: ");
            string blogContent = Console.ReadLine();
            string query = "INSERT INTO Blog (BlogTitle, BlogAuthor, BlogContent, DeleteFlag) VALUES (@BlogTitle, @BlogAuthor, @BlogContent, 0)";
            _dapperService.Execute(query, new DapperBlogModel
            {
                BlogTitle = blogTitle,
                BlogAuthor = blogAuthor,
                BlogContent = blogContent
            });
            Console.WriteLine("Blog Created Successfully!");

            string selectQuery = "SELECT top 1 * FROM Blog Order By BlogId Desc";
            var blogs = _dapperService.Query<DapperBlogModel>(selectQuery);
            if (blogs.Count > 0)
            {
                var blog = blogs.First();
                Console.WriteLine("New Blog Id: " + blog.BlogId);
                Console.WriteLine("Blog Title: " + blog.BlogTitle);
                Console.WriteLine("Blog Author: " + blog.BlogAuthor);
                Console.WriteLine("Blog Content: " + blog.BlogContent);
            }
            else
            {
                Console.WriteLine("No blogs found.");
            }
        }

        public void Edit()
        {
            Console.Write("Enter Your Blog Id: ");
            int blogId = Convert.ToInt32(Console.ReadLine());
            string query = "SELECT * FROM Blog WHERE BlogId = @blogId";
            var blogs = _dapperService.Query<DapperBlogModel>(query, new DapperBlogModel { BlogId = blogId });
            if (blogs.Count > 0)
            {
                var blog = blogs.First();
                Console.Write("Enter New Blog Title: ");
                blog.BlogTitle = Console.ReadLine();
                Console.Write("Enter New Blog Author: ");
                blog.BlogAuthor = Console.ReadLine();
                Console.Write("Enter New Blog Content: ");
                blog.BlogContent = Console.ReadLine();
                query = "UPDATE Blog SET BlogTitle = @BlogTitle, BlogAuthor = @BlogAuthor, BlogContent = @BlogContent WHERE BlogId = @BlogId";
                _dapperService.Execute(query, blog);
                Console.WriteLine("Blog Updated Successfully!");
            }
            else
            {
                Console.WriteLine("Blog not found!");
            }
        }   
    }
}
