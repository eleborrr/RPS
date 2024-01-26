using RPS.Application.Services.Abstractions.Cqrs.Queries;
using RPS.Shared.Rating;

namespace RPS.Application.Features.Rating.GetRating;

public record GetUserRatingFromMongoQuery(string Key): IQuery<UserRatingMongoDto>;