using RPS.Application.Services.Abstractions.Cqrs.Commands;
using RPS.Shared.Rating;

namespace RPS.Application.Features.MongoDb.SaveRating;

public record SaveUserRatingMongoCommand(UserRatingMongoDto UserRatingDto): ICommand;