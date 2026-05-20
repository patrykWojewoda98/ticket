using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketCommands.DeleteTicket;

public record DeleteTicketCommand(
  int TicketId
) : IRequest<TicketDto>;
