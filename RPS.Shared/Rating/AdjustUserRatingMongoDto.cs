namespace RPS.Shared.Rating;

public class AdjustUserRatingMongoDto
{
    public string UserId { get; set; }
    public int Adjust { get; set; }
};