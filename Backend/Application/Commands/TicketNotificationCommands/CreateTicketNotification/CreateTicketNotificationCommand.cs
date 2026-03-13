using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketNotificationCommands.CreateTicketNotification;

public record CreateTicketNotificationCommand(TicketNotification TicketNotification) : IRequest<Unit>;
