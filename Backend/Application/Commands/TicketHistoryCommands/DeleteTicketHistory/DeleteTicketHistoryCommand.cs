using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketHistoryCommands.DeleteTicketHistory;

public record DeleteTicketHistoryCommand(
  int TicketHistoryId
) : IRequest<TicketHistoryDto>;
