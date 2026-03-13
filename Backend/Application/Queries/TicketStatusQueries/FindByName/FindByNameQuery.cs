using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketStatusQueries.FindByName;

public record FindByNameQuery(string Name) : IRequest<TicketStatusDto>;
