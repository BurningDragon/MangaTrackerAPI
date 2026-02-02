using MangaTracker.Models;
using MangaTracker.Repository;
using MangaTracker.Repository.Interfaces;
using MangaTracker.Repository.Repositories;
using MangaTracker.Services;
using MangaTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddDbContext<MangaTrackerDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICreatorRepository, CreatorRepository>();

builder.Services.AddScoped<ICreatorService, CreatorService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MangaTracker",
        Description = "An ASP.NET Core Web API for managing mangas and light novels."
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MangaTracker API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
