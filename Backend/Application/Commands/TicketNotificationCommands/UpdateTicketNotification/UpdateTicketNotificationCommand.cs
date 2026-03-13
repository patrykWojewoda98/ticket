using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketNotificationCommands.UpdateTicketNotification;

public record UpdateTicketNotificationCommand(TicketNotification TicketNotification) : IRequest<Unit>;
