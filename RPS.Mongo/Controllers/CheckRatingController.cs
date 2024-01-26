using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RPS.Domain.Repositories.Abstractions;
using RPS.Infrastructure.MongoClient;
using RPS.Shared.Rating;
using IMediator = MassTransit.Mediator.IMediator;


namespace RPS.Mongo.Controllers;

[ApiController]
[Route("[controller]")]
public class CheckRatingController : Controller
{
    private readonly IMongoDbClient _mongoClient;
    
    public CheckRatingController(IMongoDbClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    [HttpGet("/userRating")]
    public async Task<JsonResult> GetUserRating([FromQuery] string id)
    {
        var res = (await _mongoClient.GetAsync()).Where(r => r.UserId == id);
        return Json(res);
    }
}