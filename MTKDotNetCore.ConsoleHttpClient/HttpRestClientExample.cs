using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MTKDotNetCore.ConsoleHttpClient
{
    public class HttpRestClientExample
    {
        private readonly string endPoint = "http://localhost:3000/todos";
        private readonly RestClient _client;

        public HttpRestClientExample()
        {
            _client = new RestClient();
        }

        public async Task ReadAsync()
        {
            RestRequest restRequest = new RestRequest(endPoint, Method.Get);
            var response = await _client.GetAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                var todos = JsonConvert.DeserializeObject(response.Content!);
                Console.WriteLine(todos);
            }
        }

        public async Task ReadAsync(int id)
        {
            RestRequest restRequest = new RestRequest(endPoint + "/" + id, Method.Get);

            var response = await _client.GetAsync(restRequest);
            var JsonStr = response.Content!;

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
            RestRequest restRequest = new RestRequest(endPoint, Method.Post);

            Todo newTodo = new Todo()
            {
                todo = todo,
                completed = completed,
                userId = userId,
            };
            
            restRequest.AddBody(newTodo);

            var response = await _client.PostAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content!);
            }
        }

        public async Task UpdateAsync(int id, string todo, bool completed, int userId)
        {
            RestRequest restRequest = new RestRequest(endPoint + "/" + id, Method.Get);
            var response = await _client.GetAsync(restRequest);
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
                RestRequest restRequest2 = new RestRequest(endPoint + "/" + id, Method.Put);
                restRequest2.AddBody(oldTodo);
                var response2 = await _client.PutAsync(restRequest2);
                if (response2.IsSuccessful)
                {
                    Console.WriteLine(response2.Content!);
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            RestRequest restRequest = new RestRequest(endPoint + "/" + id, Method.Delete);
            var response = await _client.DeleteAsync(restRequest);

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
}
