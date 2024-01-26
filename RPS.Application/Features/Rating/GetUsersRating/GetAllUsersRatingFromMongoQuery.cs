using RPS.Application.Services.Abstractions.Cqrs.Queries;
using RPS.Shared.Rating;

namespace RPS.Application.Features.Rating.GetUsersRating;

public class GetAllUsersRatingFromMongoQuery : IQuery<IEnumerable<UserRatingMongoDto>>
{
    
}