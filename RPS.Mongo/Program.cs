using Microsoft.AspNetCore.Http.HttpResults;
using RPS.Application.Helpers;
using RPS.Mongo.Services.Rating;
using RPS.Mongo.ServicesExtensions.Mongo;
using RPS.Mongo.ServicesExtensions.RabbitMq;
using Microsoft.AspNetCore.Mvc;
using RPS.Infrastructure.MongoClient;
using RPS.Shared.Rating;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMongo(builder.Configuration);
builder.Services.AddMasstransitRabbitMq(builder.Configuration);
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddScoped<RatingGetterService>();

var testSpesific = "testSpesific";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: testSpesific, policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod();
        policyBuilder.WithOrigins("https://oauth.vk.com")
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod();
        policyBuilder.WithOrigins("https://localhost:7015")
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod();
    });

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("api/ratings", async ([FromServices] RatingGetterService storageService) =>
{
    var allRatings = await storageService.GetRatings();
    return allRatings is not null
        ? Results.Ok(allRatings.OrderBy(r => r.Rating))
        : Results.NotFound();
});

app.MapGet("api/addrating", async ([FromServices] IMongoDbClient mongoDbClient) =>
{
    await mongoDbClient.CreateAsync(new UserRatingMongoDto(){UserId = "2", Rating = 150});
    return Results.Ok();
});

app.MapControllers();



app.Run();