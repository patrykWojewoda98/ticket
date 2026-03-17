using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketQueries.GetAllTickets;

public record GetAllTicketsQuery() : IRequest<List<TicketDto>>;
