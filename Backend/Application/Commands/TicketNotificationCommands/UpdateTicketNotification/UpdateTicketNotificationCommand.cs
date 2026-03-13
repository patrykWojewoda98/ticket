using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketNotificationCommands.UpdateTicketNotification;

public record UpdateTicketNotificationCommand(
  int TicketNotificationId,
  string? Message,
  bool? Read
) : IRequest<TicketNotificationDto>;
