using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketHistoryQueries.GetAllTicketHistories;

public record GetAllTicketHistoriesQuery() : IRequest<List<TicketHistoryDto>>;
