using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketNotificationQueries.GetTicketNotificationById;

public record GetTicketNotificationByIdQuery(int Id) : IRequest<TicketNotificationDto>;
