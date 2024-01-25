namespace RPS.Domain.Entities;

public record Match(string Id, string FirstUserId, string SecondUserId, string FirstUserMoveId, string SecondUserMoveId);