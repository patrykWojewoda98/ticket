using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketCommands.UpdateTicket;

public record UpdateTicketCommand(
  int TicketId,
  int? AssigneeId,
  int? CategoryId,
  int? StatusId,
  int? PriorityId,
  string? Title,
  string? Description
) : IRequest<TicketDto>;
