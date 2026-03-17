using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketStatusQueries.GetAllTicketStatuses;

public record GetAllTicketStatusesQuery() : IRequest<List<TicketStatusDto>>;
