using RPS.Application.Dto.MediatR;
using RPS.Application.Services.Abstractions.Cqrs.Queries;
using RPS.Shared.Rating;

namespace RPS.Application.Features.Rating.GetUsersRating;

public class GetAllUsersRatingFromMongoQueryHandler: IQueryHandler<GetAllUsersRatingFromMongoQuery, IEnumerable<UserRatingMongoDto>>
{
    public Task<Result<IEnumerable<UserRatingMongoDto>>> Handle(GetAllUsersRatingFromMongoQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}