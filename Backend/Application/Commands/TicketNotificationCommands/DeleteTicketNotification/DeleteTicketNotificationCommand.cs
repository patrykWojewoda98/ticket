using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketNotificationCommands.DeleteTicketNotification;

public record DeleteTicketNotificationCommand(TicketNotification TicketNotification) : IRequest<Unit>;
