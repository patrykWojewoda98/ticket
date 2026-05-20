using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketStatusQueries.GetTicketStatusById;

public record GetTicketStatusByIdQuery(int Id) : IRequest<TicketStatusDto>;
