using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketHistoryCommands.UpdateTicketHistory;

public record UpdateTicketHistoryCommand(
  int TicketHistoryId,
  string? Action,
  string? OldValue,
  string? NewValue
) : IRequest<TicketHistoryDto>;
