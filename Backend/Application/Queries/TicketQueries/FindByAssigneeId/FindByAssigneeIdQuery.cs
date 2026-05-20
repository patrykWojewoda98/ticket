using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketQueries.FindByAssigneeId;

public record FindByAssigneeIdQuery(int AssigneeId) : IRequest<List<TicketDto>>;
