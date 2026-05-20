using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketPriorityCommands.DeleteTicketPriority;

public record DeleteTicketPriorityCommand(
  int TicketPriorityId
) : IRequest<TicketPriorityDto>;
