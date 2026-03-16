using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.SessionCommands.CreateSession;

public record CreateSessionCommand(
  int UserId,
  string Token,
  DateTime ExpiresAt,
  string? IpAddress,
  string? UserAgent
) : IRequest<SessionDto>;
