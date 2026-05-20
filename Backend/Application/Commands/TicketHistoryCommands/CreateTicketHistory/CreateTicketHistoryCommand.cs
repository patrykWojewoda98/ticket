using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketHistoryCommands.CreateTicketHistory;

public record CreateTicketHistoryCommand(
  int TicketId,
  int UserId,
  string Action,
  string? OldValue,
  string? NewValue
) : IRequest<TicketHistoryDto>;
