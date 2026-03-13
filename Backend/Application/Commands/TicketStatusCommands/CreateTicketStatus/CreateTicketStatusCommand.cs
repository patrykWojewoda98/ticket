using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketStatusCommands.CreateTicketStatus;

public record CreateTicketStatusCommand(TicketStatus TicketStatus) : IRequest<Unit>;
