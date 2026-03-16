using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketPriorityCommands.CreateTicketPriority;

public record CreateTicketPriorityCommand(
  string Name
) : IRequest<TicketPriorityDto>;
