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
        Console.WriteLine("asd");
        var ratingInMongo = await _mongoDbClient.GetAsync(context.Message.UserId);
        if (ratingInMongo is null)
            await _mongoDbClient.CreateAsync(new UserRatingMongoDto 
                {UserId = context.Message.UserId, Rating = context.Message.Adjust});
        else
        {
            await _mongoDbClient.UpdateAsync(context.Message.UserId,
                new UserRatingMongoDto
                    {Id = ratingInMongo.Id, UserId = ratingInMongo.UserId, Rating = ratingInMongo.Rating + context.Message.Adjust });
        }
    }
}