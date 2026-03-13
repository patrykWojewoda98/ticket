using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketPriorityQueries.FindByName;

public record FindByNameQuery(string Name) : IRequest<TicketPriorityDto>;
