using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketStatusCommands.UpdateTicketStatus;

public record UpdateTicketStatusCommand(
  int TicketStatusId,
  string? Name
) : IRequest<TicketStatusDto>;
