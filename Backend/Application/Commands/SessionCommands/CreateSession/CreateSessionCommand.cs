using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.SessionCommands.CreateSession;

public record CreateSessionCommand(Session Session) : IRequest<Unit>;
