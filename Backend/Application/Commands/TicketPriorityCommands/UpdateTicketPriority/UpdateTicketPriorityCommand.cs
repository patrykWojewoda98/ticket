using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketPriorityCommands.UpdateTicketPriority;

public record UpdateTicketPriorityCommand(
  int TicketPriorityId,
  string? Name
) : IRequest<TicketPriorityDto>;
