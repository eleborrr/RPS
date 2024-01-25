namespace RPS.Domain.Entities;

public class Match
{
    public string Id { get; set; }
    public string FirstUserId { get; set; }
    public string SecondUserId { get; set; }
    public string FirstUserMoveId { get; set; }
    public string SecondUserMoveId { get; set; }
}