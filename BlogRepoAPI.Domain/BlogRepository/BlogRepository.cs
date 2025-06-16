using BlogRepoAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BlogRepoAPI.Domain.BlogRepository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly Database.Models.AppContext context;

        public BlogRepository()
        {
            this.context = new Database.Models.AppContext();
        }

        public async Task<Blog> GetBlogByIdAsync(int blogId)
        {
            return await context.Blogs.FirstAsync(x => x.BlogId == blogId);
        }

        public async Task<List<Blog>> GetBlogsAsync()
        {
            return await context.Blogs.ToListAsync();
        }

        public async Task AddBlogAsync(Blog blog)
        {
            Blog newBlog = new Blog();
            newBlog.BlogTitle = blog.BlogTitle; 
            newBlog.BlogAuthor = blog.BlogAuthor; 
            newBlog.BlogContent = blog.BlogContent; 
            newBlog.DeleteFlag = false;
            await context.Blogs.AddAsync(newBlog);
            await context.SaveChangesAsync();
        }

        public async Task UpdateBlogAsync(Blog blog)
        {
            var existingBlog = await context.Blogs
                .FirstOrDefaultAsync(x => x.BlogId == blog.BlogId);

            if (existingBlog == null)
                return; // Or throw an exception, depending on your logic

            // Update only if input values are not null or empty
            existingBlog.BlogTitle = !string.IsNullOrWhiteSpace(blog.BlogTitle)
                ? blog.BlogTitle
                : existingBlog.BlogTitle;

            existingBlog.BlogAuthor = !string.IsNullOrWhiteSpace(blog.BlogAuthor)
                ? blog.BlogAuthor
                : existingBlog.BlogAuthor;

            existingBlog.BlogContent = !string.IsNullOrWhiteSpace(blog.BlogContent)
                ? blog.BlogContent
                : existingBlog.BlogContent;

            existingBlog.DeleteFlag = false;

            context.Blogs.Update(existingBlog);
            await context.SaveChangesAsync();
        }


        public async Task DeleteBlogAsync(int blogId)
        {
            Blog existBlog = context.Blogs.First(x => x.BlogId == blogId);
            if (existBlog != null)
            {
                existBlog.DeleteFlag = true;

                context.Blogs.Update(existBlog);
                await context.SaveChangesAsync();
            }
        }
    }
}
