using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketStatusCommands.CreateTicketStatus;

public record CreateTicketStatusCommand(
  string Name
) : IRequest<TicketStatusDto>;
