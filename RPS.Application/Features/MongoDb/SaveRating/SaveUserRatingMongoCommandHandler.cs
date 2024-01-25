using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using RPS.Application.Dto.MediatR;
using RPS.Application.Services.Abstractions;
using RPS.Infrastructure.MongoClient;

namespace RPS.Application.Features.MongoDb.SaveRating;

public class SaveUserRatingMongoCommandHandler: ICommandHandler<SaveUserRatingMongoCommand>
{
    private readonly IMongoDbClient _mongoClient;

    public SaveUserRatingMongoCommandHandler(IMongoDbClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    public Task<Result> Handle(SaveUserRatingMongoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _mongoClient.CreateAsync(request.UserRatingDto);
            return Task.FromResult(new Result(true));
        }
        catch (Exception e)
        {
            return Task.FromResult(new Result(false, e.Message));
        }
    }
}