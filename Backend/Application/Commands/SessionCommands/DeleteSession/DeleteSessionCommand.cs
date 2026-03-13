using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.SessionCommands.DeleteSession;

public record DeleteSessionCommand(Session Session) : IRequest<Unit>;
