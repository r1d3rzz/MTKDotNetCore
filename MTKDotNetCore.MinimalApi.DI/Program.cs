using Microsoft.AspNetCore.Mvc;
using Refit;
using RestSharp;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//for httpclient
builder.Services.AddSingleton(x => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetSection("BirdAPI").Value!)
});

//for restclient
builder.Services.AddSingleton(x => new RestClient(builder.Configuration.GetSection("BirdAPI").Value!));

//for refit
builder.Services
    .AddRefitClient<IBirdApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetSection("BirdAPI").Value!));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region HttpClientExample
//app.MapGet("/birds", async ([FromServices] HttpClient httpClient) =>
//{
//    var res = await httpClient.GetAsync("birds");
//    return res.Content.ReadAsStringAsync();
//});

//app.MapGet("/birds/{id:int}", async ([FromServices] HttpClient httpClient, int id) =>
//{
//    var res = await httpClient.GetAsync("birds/" + id);
//    return res.Content.ReadAsStringAsync();
//});
#endregion

#region RestSharpExample
//app.MapGet("/birds", async ([FromServices] RestClient restClient) =>
//{
//    RestRequest res = new RestRequest("/birds", Method.Get);
//    var response = await restClient.GetAsync(res);
//    return response.Content;
//});

//app.MapGet("/birds/{id:int}", async ([FromServices] RestClient restClient, int id) =>
//{
//    RestRequest res = new RestRequest("/birds/" + id, Method.Get);
//    var response = await restClient.GetAsync(res);
//    return response.Content;
//});
#endregion

#region RefitExample

app.MapGet("/birds", async ([FromServices] IBirdApi birdApi) =>
{
    var birds = await birdApi.GetBirds();
    return Results.Ok(birds);
});

app.MapGet("/birds/{id:int}", async ([FromServices] IBirdApi birdApi, int id) =>
{
    var bird = await birdApi.GetBirds(id);
    return Results.Ok(bird);
});

#endregion

app.Run();

public interface IBirdApi
{
    [Get("/birds")]
    Task<List<BirdModel>> GetBirds();

    [Get("/birds/{id}")]
    Task<BirdModel> GetBirds(int id);
}

public class BirdModel
{
    public int Id { get; set; }
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}
