using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketStatusQueries.GetAllTicketStatus;

public record GetAllTicketStatusQuery() : IRequest<List<TicketStatusDto>>;
