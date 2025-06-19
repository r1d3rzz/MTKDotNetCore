using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.ConsoleApp.Model;

namespace BlogMinimalApi.EndPoints.Blog
{
    public static class BlogEndPoint
    {
        public static void UseBlogEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/blogs", (DotNetTrainingBatch5Context context) =>
            {
                var blogs = context.Blogs.Where(x => x.DeleteFlag != true).ToList();
                return Results.Ok(blogs);
            }).WithName("GetBlogs").WithOpenApi();

            app.MapGet("/blogs/{id}", (DotNetTrainingBatch5Context context, int id) =>
            {
                var blog = context.Blogs.Where(x => x.BlogId == id).FirstOrDefault();
                if (blog != null)
                {
                    return Results.Ok(blog);
                }

                return Results.NotFound();
            }).WithName("ShowBlog").WithOpenApi();

            app.MapPost("/blogs/create", (DotNetTrainingBatch5Context context, MTKDotNetCore.ConsoleApp.Model.Blog blog) =>
            {
                var newBlog = new MTKDotNetCore.ConsoleApp.Model.Blog();
                newBlog.BlogTitle = blog.BlogTitle;
                newBlog.BlogAuthor = blog.BlogAuthor;
                newBlog.BlogContent = blog.BlogContent;
                newBlog.DeleteFlag = false;

                context.Blogs.Add(newBlog);
                context.SaveChanges();
                return Results.Ok(newBlog);
            }).WithName("CreateBlog").WithOpenApi();

            app.MapPut("/blogs/edit/{id}", (DotNetTrainingBatch5Context context, int id ,MTKDotNetCore.ConsoleApp.Model.Blog blog) =>
            {
                var editBlog = context.Blogs.AsNoTracking().Where(x => x.BlogId == id).FirstOrDefault();

                if (editBlog is null)
                {
                    return Results.NotFound();
                }

                editBlog.BlogTitle = blog.BlogTitle;
                editBlog.BlogAuthor = blog.BlogAuthor;
                editBlog.BlogContent = blog.BlogContent;
                editBlog.DeleteFlag = blog.DeleteFlag;

                context.Entry(editBlog).State = EntityState.Modified;
                context.SaveChanges();

                return Results.Ok(editBlog);
            }).WithName("PutBlog").WithOpenApi();
        }
    }
}
