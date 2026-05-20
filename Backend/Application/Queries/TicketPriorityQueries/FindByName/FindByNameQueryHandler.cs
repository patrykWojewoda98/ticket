using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketPriorityQueries.FindByName;

public class FindByNameQueryHandler : IRequestHandler<FindByNameQuery, TicketPriorityDto>
{
  private readonly ITicketPriorityRepository _repository;

  public FindByNameQueryHandler(ITicketPriorityRepository repository)
  {
    _repository = repository;
  }

  public async Task<TicketPriorityDto> Handle(FindByNameQuery request, CancellationToken cancellationToken)
  {
    var ticketPriority = await _repository.FindByNameAsync(request.Name);
    if (ticketPriority == null) return null;

    return new TicketPriorityDto
    {
      Id = ticketPriority.Id,
      Name = ticketPriority.Name
    };
  }
}
