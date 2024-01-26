namespace RPS.Domain.Entities;

public class GameRoom
{
    public string Id { get; set; } = default!;
    public int TimeToMove  { get; set; }
    public int EloDelta { get; set; } = default!;
    public string CreatorId { get; set; } = default!;
    public string? Participant { get; set; } = default!;
    public DateTime CreationDate { get; set; }
    public bool IsStarted { get; set; }
}