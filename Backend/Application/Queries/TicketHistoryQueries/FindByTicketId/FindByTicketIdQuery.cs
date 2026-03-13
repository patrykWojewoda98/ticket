using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketHistoryQueries.FindByTicketId;

public record FindByTicketIdQuery(int TicketId) : IRequest<List<TicketHistoryDto>>;
