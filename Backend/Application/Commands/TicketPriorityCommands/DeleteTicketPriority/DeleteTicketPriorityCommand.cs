using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketPriorityCommands.DeleteTicketPriority;

public record DeleteTicketPriorityCommand(TicketPriority TicketPriority) : IRequest<Unit>;
