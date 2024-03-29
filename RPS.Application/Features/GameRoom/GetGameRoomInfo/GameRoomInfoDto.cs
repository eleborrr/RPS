﻿namespace RPS.Application.Features.GameRoom.GetGameRoomInfo;

public record GameRoomInfoDto(string CreatorName, DateTime CreationDate, string GameRoomId, string CreatorId, string ParticipantId, bool CreatorConnected, bool ParticipantConnected);
