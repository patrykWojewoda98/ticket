using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketPriorityQueries.GetTicketPriorityById;

public record GetTicketPriorityByIdQuery(int Id) : IRequest<TicketPriorityDto>;
