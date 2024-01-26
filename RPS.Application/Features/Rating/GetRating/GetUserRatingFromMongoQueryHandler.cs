// using BeaverTinder.Application.Clients.MongoClient;

using RPS.Application.Dto.MediatR;
using RPS.Application.Services.Abstractions.Cqrs.Queries;
using RPS.Infrastructure.MongoClient;
using RPS.Shared.Rating;

namespace RPS.Application.Features.Rating.GetRating;

public class GetUserRatingFromMongoQueryHandler: IQueryHandler<GetUserRatingFromMongoQuery, UserRatingMongoDto>
{
    private readonly IMongoDbClient _mongoDb;

    public GetUserRatingFromMongoQueryHandler(IMongoDbClient mongoDb)
    {
        _mongoDb = mongoDb;
    }

    public async Task<Result<UserRatingMongoDto>> Handle(GetUserRatingFromMongoQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var metadata = await _mongoDb.GetAsync(request.Key);
            return new Result<UserRatingMongoDto>(metadata, true);
        }
        catch (Exception e)
        {
            return new Result<UserRatingMongoDto>(null, false, e.Message);
        }
    }
}