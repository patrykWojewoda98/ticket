using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketNotificationCommands.DeleteTicketNotification;

public record DeleteTicketNotificationCommand(
  int TicketNotificationId
) : IRequest<TicketNotificationDto>;
