using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketNotificationQueries._FindByTicketId;

public record FindByTicketIdQuery(int TicketId) : IRequest<List<TicketNotificationDto>>;
