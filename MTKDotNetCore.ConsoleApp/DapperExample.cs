using Dapper;
using MTKDotNetCore.ConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.ConsoleApp
{
    public class DapperExample
    {
        private string _connectionString = "Data Source=DESKTOP-QREHFRH;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=rider";

        public void Read()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Blog WHERE DeleteFlag != @deleteFlag";
                var blogs = db.Query(query, new
                {
                    deleteFlag = 1
                }).ToList();
                foreach (var blog in blogs)
                {
                    Console.WriteLine(blog.BlogId);
                    Console.WriteLine(blog.BlogAuthor);
                    Console.WriteLine(blog.BlogContent);
                    Console.WriteLine(blog.DeleteFlag);
                    Console.WriteLine("=====================================");
                }
            }
        }

        public void Create()
        {
            Console.Write("Enter Blog Title: ");
            string blogTitle = Console.ReadLine();

            Console.Write("Enter Blog Author: ");
            string blogAuthor = Console.ReadLine();

            Console.Write("Enter Blog Content: ");
            string blogContent = Console.ReadLine();

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Blog (BlogTitle, BlogAuthor, BlogContent, DeleteFlag) VALUES (@BlogTitle, @BlogAuthor, @BlogContent, 0)";
                db.Execute(query, new BlogModel
                {
                    BlogTitle = blogTitle,
                    BlogAuthor = blogAuthor,
                    BlogContent = blogContent
                });
                Console.WriteLine("Blog Created Successfully!");
            }
        }

        public void Edit()
        {
            Console.Write("Enter Your Blog Id: ");
            int blogId = Convert.ToInt32(Console.ReadLine());

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Blog WHERE BlogId = @BlogId";
                BlogModel blog = db.Query<BlogModel>(query, new BlogModel
                {
                    BlogId = blogId
                }).FirstOrDefault();

                if (blog == null)
                {
                    Console.WriteLine("Your Blog Not Found");
                    return;
                }
                else
                {
                    Console.WriteLine($"Blog {blog.BlogTitle}");
                    Console.WriteLine($"Author {blog.BlogAuthor}");
                    Console.WriteLine($"Content {blog.BlogContent}");
                    Console.WriteLine($"IsActive {blog.DeleteFlag}");
                    Console.WriteLine("====================================");


                    Console.Write("Do you want to Edit this Blog (Y/N) : ");
                    bool isEdit = Console.ReadLine().ToLower() == "y" ? true : false;
                    if (!isEdit)
                    {
                        Console.WriteLine("User Not Edit");
                    }
                    else
                    {
                        Console.Write("Enter Blog Title: ");
                        string blogTitle = Console.ReadLine();

                        Console.Write("Enter Blog Author: ");
                        string blogAuthor = Console.ReadLine();

                        Console.Write("Enter Blog Content: ");
                        string blogContent = Console.ReadLine();

                        string updateQuery = "UPDATE Blog SET BlogTitle = @BlogTitle, BlogAuthor = @BlogAuthor, BlogContent = @BlogContent WHERE BlogId = @BlogId";
                        db.Execute(updateQuery, new BlogModel
                        {
                            BlogTitle = blogTitle,
                            BlogAuthor = blogAuthor,
                            BlogContent = blogContent,
                            BlogId = blogId
                        });
                        Console.WriteLine("Blog Updated Successfully!");
                    }
                }
            }

        }

        public void Delete()
        {
            Console.Write("Enter Your Blog Id: ");
            int blogId = Convert.ToInt32(Console.ReadLine());

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Blog WHERE BlogId = @BlogId";
                BlogModel blog = db.Query<BlogModel>(query, new BlogModel
                {
                    BlogId = blogId
                }).FirstOrDefault();

                if (blog == null)
                {
                    Console.WriteLine("Your Blog Not Found");
                    return;
                }
                else
                {
                    string deleteQuery = "DELETE Blog WHERE BlogId = @BlogId";
                    db.Execute(deleteQuery, new BlogModel
                    {
                        BlogId = blogId
                    });
                    Console.WriteLine("Delete Blog Success");
                }
            }
        }
    }
}
