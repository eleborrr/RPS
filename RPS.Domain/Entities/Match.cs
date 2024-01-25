namespace RPS.Domain.Entities;

public class Match
{
    public string Id { get; set; } = default!;
    public string FirstUserId { get; set; } = default!;
    public string SecondUserId { get; set; } = default!;
    public string? FirstUserMoveId { get; set; }
    public string? SecondUserMoveId { get; set; }
}