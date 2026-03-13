using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.SessionCommands.DeleteSession;

public record DeleteSessionCommand(
  int SessionId
) : IRequest<SessionDto>;
