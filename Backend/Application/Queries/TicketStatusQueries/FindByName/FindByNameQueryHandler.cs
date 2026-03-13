using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketStatusQueries.FindByName;

public class FindByNameQueryHandler : IRequestHandler<FindByNameQuery, TicketStatusDto>
{
  private readonly ITicketStatusRepository _repository;

  public FindByNameQueryHandler(ITicketStatusRepository repository)
  {
    _repository = repository;
  }

  public async Task<TicketStatusDto> Handle(FindByNameQuery request, CancellationToken cancellationToken)
  {
    var ticketStatus = await _repository.FindByNameAsync(request.Name);
    if (ticketStatus == null) return null;

    return new TicketStatusDto
    {
      Id = ticketStatus.Id,
      Name = ticketStatus.Name
    };
  }
}
