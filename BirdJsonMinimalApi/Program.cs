using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

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

app.MapGet("/birds", () =>
{
    string folderPath = "Data/Birds.json";
    List<Tbl_Bird> birds = JsonConvert.DeserializeObject<Bird>(File.ReadAllText(folderPath)).Tbl_Bird!.ToList();

    return Results.Ok(birds);
}).WithName("GetBirds").WithOpenApi();

app.MapGet("/birds/{id}", (int id) =>
{
    string folderPath = "Data/Birds.json";
    List<Tbl_Bird> birds = JsonConvert.DeserializeObject<Bird>(File.ReadAllText(folderPath)).Tbl_Bird!.ToList();

    if (birds != null)
    {
        return Results.Ok(birds.FirstOrDefault(b => b.Id == id));
    }

    return Results.NotFound();
}).WithName("GetBird").WithOpenApi();


app.MapPost("/birds", (Tbl_Bird tbl_Bird) =>
{
    string folderPath = "Data/Birds.json";
    List<Tbl_Bird> birds = JsonConvert.DeserializeObject<Bird>(File.ReadAllText(folderPath)).Tbl_Bird!.ToList();

    Tbl_Bird newBird = new Tbl_Bird();
    newBird.Id = birds.Count < 0 ? 1 : birds.Max(b => b.Id) + 1;
    newBird.BirdMyanmarName = tbl_Bird.BirdMyanmarName;
    newBird.BirdEnglishName = tbl_Bird.BirdEnglishName;
    newBird.Description = tbl_Bird.Description;
    newBird.ImagePath = tbl_Bird.ImagePath;
    birds.Add(newBird);
    var data = new
    {
        Tbl_Bird = birds
    };

    File.WriteAllText(folderPath, JsonConvert.SerializeObject(data, Formatting.Indented));
    return Results.Ok(newBird);

});


app.Run();


public class Bird
{
    public List<Tbl_Bird>? Tbl_Bird { get; set; }
}

public class Tbl_Bird
{
    public int Id { get; set; }
    public string? BirdMyanmarName { get; set; }
    public string? BirdEnglishName { get; set; }
    public string? Description { get; set; }
    public string? ImagePath { get; set; }
}
