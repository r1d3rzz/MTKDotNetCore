using MTKDotNetCore.AuthAPI.ActionFilters;
using MTKDotNetCore.AuthAPI.Helpers;
using MTKDotNetCore.AuthAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<EncDecHelper>();
builder.Services.AddScoped<ValidationTokenFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseValidationTokenMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
