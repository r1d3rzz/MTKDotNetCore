using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.ConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.ConsoleApp
{
    public class EFCoreExample
    {
        private readonly DotNetTrainingBatch5Context context;

        public EFCoreExample(DotNetTrainingBatch5Context context)
        {
            this.context = context;
        }

        public void Read()
        {
            List<Blog> blogs = context.Blogs.Where(x => x.DeleteFlag != true).ToList();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine(blog.BlogTitle);
                Console.WriteLine(blog.BlogAuthor);
                Console.WriteLine(blog.BlogContent);
                Console.WriteLine(blog.DeleteFlag);
                Console.WriteLine("=====================================");
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
            Blog blog = new Blog
            {
                BlogTitle = blogTitle,
                BlogAuthor = blogAuthor,
                BlogContent = blogContent,
                DeleteFlag = false // 0 = Not Deleted, 1 = Deleted
            };
            context.Blogs.Add(blog);
            context.SaveChanges();
            Console.WriteLine("Blog Created Successfully!");
        }

        public void Edit()
        {
            Console.Write("Enter Blog ID to Edit: ");
            int blogId = int.Parse(Console.ReadLine());
            Blog blog = context.Blogs.FirstOrDefault(x => x.BlogId == blogId && x.DeleteFlag != true);
            if (blog != null)
            {
                Console.WriteLine(blog.BlogTitle);
                Console.WriteLine(blog.BlogAuthor);
                Console.WriteLine(blog.BlogContent);
                Console.WriteLine(blog.DeleteFlag);
                Console.WriteLine("=====================================");

                Console.Write("Do you want to edit this blog? (Y/N): ");
                bool isEdit = Console.ReadLine().ToLower() == "y";

                if (!isEdit)
                {
                    Console.WriteLine("Edit Cancelled!");
                    return;
                }
                else
                {
                    Console.Write("Enter New Blog Title: ");
                    blog.BlogTitle = Console.ReadLine();
                    Console.Write("Enter New Blog Author: ");
                    blog.BlogAuthor = Console.ReadLine();
                    Console.Write("Enter New Blog Content: ");
                    blog.BlogContent = Console.ReadLine();
                    context.SaveChanges();
                    Console.WriteLine("Blog Updated Successfully!");
                }
            }
            else
            {
                Console.WriteLine("Blog Not Found or Already Deleted!");
            }
        }

        public void Delete()
        {
            Console.Write("Enter Blog ID to Delete: ");
            int blogId = int.Parse(Console.ReadLine());
            Blog blog = context.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == blogId && x.DeleteFlag != true);
            if (blog != null)
            {
                blog.DeleteFlag = true;
                context.Entry(blog).State = EntityState.Modified;
                context.SaveChanges();
                Console.WriteLine("Blog Deleted Successfully!");
            }
            else
            {
                Console.WriteLine("Blog Not Found or Already Deleted!");
            }
        }
    }
}
