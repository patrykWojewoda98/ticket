using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketQueries.GetTicketById;

public record GetTicketByIdQuery(int Id) : IRequest<TicketDto>;
