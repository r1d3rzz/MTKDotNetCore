using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.ConsoleHttpClient
{
    public class RefitExample
    {
        private readonly ITodoRefit _api;

        public RefitExample()
        {
            _api = RestService.For<ITodoRefit>("http://localhost:3000");
        }

        public async Task Todos()
        {
            try
            {
                var todos = await _api.GetTodos();
                if (todos.Count() > 0)
                {
                    foreach (var todo in todos)
                    {
                        Console.WriteLine(todo.id + ". " + todo.todo);
                    }
                }
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task Todos(string id)
        {
            try
            {
                var todo = await _api.GetTodo(id);
                if (todo != null)
                {
                    Console.WriteLine(todo.id + ". " + todo.todo);
                }
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task CreateTodo(string id, string todo, bool completed, int userId)
        {
            try
            {
                var newTodo = await _api.CreateTodo(new Models.Todo
                {
                    id = id,
                    todo = todo,
                    completed = completed,
                    userId = userId,
                });

                Console.WriteLine(newTodo.id + ". " + newTodo.todo);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task UpdateTodo(string id, string todo, bool completed, int userId)
        {
            try
            {
                var existTodo = await _api.GetTodo(id);

                if (existTodo != null)
                {
                    var newTodo = await _api.UpdateTodo(id, new Models.Todo
                    {
                        id = id,
                        todo = todo,
                        completed = completed,
                        userId = userId,
                    });

                    Console.WriteLine(newTodo.id + ". " + newTodo.todo);
                }
                else
                {
                    Console.WriteLine("Todo Not Found");
                }
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task DeleteTodo(string id)
        {
            try
            {
                var existTodo = await _api.GetTodo(id);

                if (existTodo != null)
                {
                    var isDeleted = await _api.DeleteTodo(id);

                    if (isDeleted != null)
                    {
                        Console.WriteLine("Delete Success");
                    }

                }
                else
                {
                    Console.WriteLine("Todo Not Found");
                }
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
