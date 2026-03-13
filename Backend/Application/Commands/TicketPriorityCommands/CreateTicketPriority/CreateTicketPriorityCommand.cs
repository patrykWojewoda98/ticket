using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketPriorityCommands.CreateTicketPriority;

public record CreateTicketPriorityCommand(TicketPriority TicketPriority) : IRequest<Unit>;
