using System;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketQueries.CountByStatusId;

public class CountByStatusIdQueryHandler : IRequestHandler<CountByStatusIdQuery, int>
{
  private readonly ITicketRepository _repository;

  public CountByStatusIdQueryHandler(ITicketRepository repository)
  {
    _repository = repository;
  }

  public async Task<int> Handle(CountByStatusIdQuery request, CancellationToken cancellationToken)
  {
    return await _repository.CountByStatusIdAsync(request.StatusId, cancellationToken);
  }
}
