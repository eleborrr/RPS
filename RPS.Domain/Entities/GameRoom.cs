namespace RPS.Domain.Entities;

public class GameRoom
{
    public string Id { get; set; } = default!;
    public int TimeToMove  { get; set; }
    public int EloDelta { get; set; } = default!;
    public string? Participant1 { get; set; } = default!;
    public string? Participant2 { get; set; } = default!;
}