namespace RPS.Domain.Entities;

public class GameRoom
{
    public string Id { get; set; } = default!;
    public string FirstParticipantId { get; set; } = default!;
    public string SecondParticipantId { get; set; } = default!;
}