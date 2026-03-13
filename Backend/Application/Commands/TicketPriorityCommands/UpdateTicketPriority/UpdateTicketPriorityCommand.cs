using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketPriorityCommands.UpdateTicketPriority;

public record UpdateTicketPriorityCommand(TicketPriority TicketPriority) : IRequest<Unit>;
