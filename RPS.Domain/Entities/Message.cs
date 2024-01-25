namespace RPS.Domain.Entities;

public class Message
{
    public string Id { get; set; } = default!;
    public string SenderId { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime Timestamp { get; set; }
    public string GameRoomId { get; set; } = default!;
}