using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketStatusCommands.UpdateTicketStatus;

public record UpdateTicketStatusCommand(TicketStatus TicketStatus) : IRequest<Unit>;
