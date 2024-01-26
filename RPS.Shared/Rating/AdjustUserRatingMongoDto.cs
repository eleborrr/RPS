namespace RPS.Shared.Rating;

public record AdjustUserRatingMongoDto(string userId, int adjust);