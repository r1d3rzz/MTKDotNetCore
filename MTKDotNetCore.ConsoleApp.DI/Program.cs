using Microsoft.Extensions.DependencyInjection;
using MTKDotNetCore.ConsoleApp.DI.BlogRepo;
using MTKDotNetCore.ConsoleApp.DI.Models;

var service = new ServiceCollection().AddSingleton<IBlogService, BlogRepository>().BuildServiceProvider();
var blogService = service.GetRequiredService<IBlogService>();

Console.WriteLine("Hello, World!");

{
    List<Blog> blogs = await blogService.GetBlogs();

    foreach (Blog blog in blogs)
    {
        Console.WriteLine(blog.BlogId + ". " + blog.BlogTitle);
    }
}

{
    Console.Write("Enter Blog Id: ");
    int blogId = int.Parse(Console.ReadLine()!);

    Blog blog = await blogService.GetBlogs(blogId);

    if (blog != null)
    {
        Console.WriteLine(blog.BlogTitle);
    }
    else
    {
        Console.WriteLine("Blog Not Found");
    }
}

Console.ReadKey();