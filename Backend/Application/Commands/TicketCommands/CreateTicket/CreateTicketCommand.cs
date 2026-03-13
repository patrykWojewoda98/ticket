using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketCommands.CreateTicket;

public record CreateTicketCommand(
  int UserId,
  int? AssigneeId,
  int? CategoryId,
  int StatusId,
  int PriorityId,
  string Title,
  string Description
) : IRequest<TicketDto>;

