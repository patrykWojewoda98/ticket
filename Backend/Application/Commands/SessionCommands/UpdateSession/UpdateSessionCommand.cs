using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.SessionCommands.UpdateSession;

public record UpdateSessionCommand(
  int SessionId,
  string? Token,
  DateTime? ExpiresAt,
  string? IpAddress,
  string? UserAgent
) : IRequest<SessionDto>;
