using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/birds", () =>
{
    string filepath = "Data/bird.json";
    if (!File.Exists(filepath))
    {
        return Results.NotFound("Birds data file not found.");
    }

    string JsonString = File.ReadAllText(filepath);
    if (string.IsNullOrEmpty(JsonString))
    {
        return Results.NotFound("Birds data is empty.");
    }   

    var birds = JsonConvert.DeserializeObject<Bird>(JsonString);

    return Results.Ok(birds.Tbl_Bird);
})
.WithName("GetBirds")
.WithOpenApi();

app.MapGet("/birds/{id}", (int id) =>
{
    string filepath = "Data/bird.json";
    if (!File.Exists(filepath))
    {
        return Results.NotFound("Birds data file not found.");
    }

    string JsonString = File.ReadAllText(filepath);
    if (string.IsNullOrEmpty(JsonString))
    {
        return Results.NotFound("Birds data is empty.");
    }

    var birds = JsonConvert.DeserializeObject<Bird>(JsonString);
    var bird = birds.Tbl_Bird.Where(x => x.Id == id).FirstOrDefault();

    if (bird is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(bird);
})
.WithName("GetBird")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


public class Bird
{
    public BirdResponse[] Tbl_Bird { get; set; }
}

public class BirdResponse
{
    public int Id { get; set; }
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}
