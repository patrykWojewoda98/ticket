using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketNotificationCommands.CreateTicketNotification;

public record CreateTicketNotificationCommand(
  int TicketId,
  int UserId,
  string Message,
  bool Read
) : IRequest<TicketNotificationDto>;
