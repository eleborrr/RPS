namespace RPS.Domain.Entities;

public class GameRoom
{
    public string Id { get; set; } = default!;
    public int TimeToMove  { get; set; }
    public int EloDelta { get; set; } = default!;
    public string ParticipantId { get; set; } = default!;
    public string CreatorId { get; set; } = default!;
    public bool ParticipantConnected { get; set; }
    public bool CreatorConnected { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsStarted { get; set; }
}