using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketQueries.FindByUserId;

public record FindByUserIdQuery(int TicketId) : IRequest<List<TicketDto>>;
