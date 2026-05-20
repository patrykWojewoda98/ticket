using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketStatusCommands.DeleteTicketStatus;

public record DeleteTicketStatusCommand(
  int TicketStatusId
) : IRequest<TicketStatusDto>;
