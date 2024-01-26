using MassTransit;
using MediatR;
using RPS.Infrastructure.MongoClient;
using RPS.Shared.Rating;

namespace RPS.Mongo.Services.Rating;

public class RatingConsumer: IConsumer<AdjustUserRatingMongoDto>
{
    private readonly IMongoDbClient _mongoDbClient;

    public RatingConsumer(IMongoDbClient mongoDbClient)
    {
        _mongoDbClient = mongoDbClient;
    }

    public async Task Consume(ConsumeContext<AdjustUserRatingMongoDto> context)
    {
        var ratingInMongo = await _mongoDbClient.GetAsync(context.Message.userId);
        if (ratingInMongo is null)
            await _mongoDbClient.CreateAsync(new UserRatingMongoDto 
                {UserId = context.Message.userId, Rating = context.Message.adjust});
        else
        {
            await _mongoDbClient.UpdateAsync(context.Message.userId,
                new UserRatingMongoDto
                    { UserId = ratingInMongo.UserId, Rating = ratingInMongo.Rating + context.Message.adjust });
        }
    }
}