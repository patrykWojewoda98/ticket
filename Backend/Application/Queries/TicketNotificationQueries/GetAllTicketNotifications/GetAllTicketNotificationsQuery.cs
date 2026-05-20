using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketNotificationQueries.GetAllTicketNotifications;

public record GetAllTicketNotificationsQuery() : IRequest<List<TicketNotificationDto>>;
