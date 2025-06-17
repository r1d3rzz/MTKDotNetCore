using MTKDotNetCore.ConsoleHttpClient.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.ConsoleHttpClient
{
    public interface ITodoRefit
    {
        [Get("/todos")]
        Task<List<Models.Todo>> GetTodos();

        [Get("/todos/{id}")]
        Task<Models.Todo> GetTodo(string id);

        [Post("/todos")]
        Task<Models.Todo> CreateTodo(Models.Todo todo);

        [Put("/todos/{id}")]
        Task<Models.Todo> UpdateTodo(string id ,Models.Todo todo);

        [Delete("/todos/{id}")]
        Task<Models.Todo> DeleteTodo(string id);
    }
}
