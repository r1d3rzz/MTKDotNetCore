using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MTKDotNetCore.ConsoleHttpClient
{
    public class HttpClientExample
    {
        private readonly string endPoint = "http://localhost:3000/todos";
        private readonly HttpClient _client;

        public HttpClientExample()
        {
            _client = new HttpClient();
        }

        public async Task ReadAsync()
        {
            var response = await _client.GetAsync(endPoint);
            var JsonStr = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var todos = JsonConvert.DeserializeObject<TodoResponse>(JsonStr)!.Todos;
                foreach (var todo in todos)
                {
                    Console.WriteLine($"ID: {todo.id}, Task: {todo.todo}, Completed: {todo.completed}, User ID: {todo.userId}");
                }
            }
        }

        public async Task ReadAsync(int id)
        {
            var response = await _client.GetAsync(endPoint + "/" + id);
            var JsonStr = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("Todo Not Found");
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                var todo = JsonConvert.DeserializeObject(JsonStr);
                Console.WriteLine(todo);
            }
        }

        public async Task CreateAsync(string todo, bool completed, int userId)
        {
            Todo newTodo = new Todo()
            {
                todo = todo,
                completed = completed,
                userId = userId,
            };

            var content = new StringContent(JsonConvert.SerializeObject(newTodo), Encoding.UTF8, Application.Json);
            var response = await _client.PostAsync(endPoint, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task UpdateAsync(int id ,string todo, bool completed, int userId)
        {
            var response = await _client.GetAsync(endPoint + "?id=" + id);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("Todo Not Found");
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                Todo oldTodo = new Todo()
                {
                    id = id,
                    todo = todo,
                    completed = completed,
                    userId = userId,
                };

                var content = new StringContent(JsonConvert.SerializeObject(oldTodo), Encoding.UTF8, Application.Json);
                var response2 = await _client.PutAsync(endPoint + "/" + id, content);

                if (response2.IsSuccessStatusCode)
                {
                    Console.WriteLine(await response2.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync(endPoint + "/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("Todo Not Found");
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Delete Success");
            }
        }
    }

    public class Todo
    {
        public int id { get; set; }
        public string todo { get; set; }
        public bool completed { get; set; }
        public int userId { get; set; }
    }

    public class TodoResponse
    {
        public Todo Todo { get; set; }
        public List<Todo> Todos { get; set; }
    }

}
