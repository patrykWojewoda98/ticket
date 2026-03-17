using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketPriorityQueries.GetAllTicketPriorities;

public record GetAllTicketPrioritiesQuery() : IRequest<List<TicketPriorityDto>>;
