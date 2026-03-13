using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketStatusCommands.DeleteTicketStatus;

public record DeleteTicketStatusCommand(TicketStatus TicketStatus) : IRequest<Unit>;
