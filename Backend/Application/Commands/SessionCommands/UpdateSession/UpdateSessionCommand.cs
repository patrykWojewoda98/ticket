using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.SessionCommands.UpdateSession;

public record UpdateSessionCommand(Session Session) : IRequest<Unit>;
