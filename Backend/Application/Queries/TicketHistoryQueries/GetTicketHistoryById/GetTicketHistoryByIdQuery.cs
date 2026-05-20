using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketHistoryQueries.GetTicketHistoryById;

public record GetTicketHistoryByIdQuery(int Id) : IRequest<TicketHistoryDto>;
