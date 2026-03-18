using System;
using MediatR;

namespace Application.Queries.TicketQueries.CountByStatusId;

public record CountByStatusIdQuery(int StatusId) : IRequest<int>;
