using MediatR;
using RPS.Infrastructure.MongoClient;
using RPS.Shared.Rating;

namespace RPS.Mongo.Services.Rating;

public class RatingGetterService
{
    private readonly IMongoDbClient _mongoDbClient;

    public RatingGetterService(IMongoDbClient mongoDbClient)
    {
        _mongoDbClient = mongoDbClient;
    }

    public async Task<IEnumerable<UserRatingMongoDto>> GetRatings()
    {
        return await _mongoDbClient.GetAsync();
    }
}