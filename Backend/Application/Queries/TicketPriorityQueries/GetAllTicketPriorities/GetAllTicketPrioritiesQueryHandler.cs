using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketPriorityQueries.GetAllTicketPriorities;

public class GetAllTicketPrioritiesQueryHandler : IRequestHandler<GetAllTicketPrioritiesQuery, List<TicketPriorityDto>>
{
  private readonly ITicketPriorityRepository _repository;

  public GetAllTicketPrioritiesQueryHandler(ITicketPriorityRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<TicketPriorityDto>> Handle(GetAllTicketPrioritiesQuery request, CancellationToken cancellationToken)
  {
    var ticketPriorities = await _repository.GetAllAsync();
    return ticketPriorities.Select(ticketPriority => new TicketPriorityDto
    {
      Id = ticketPriority.Id,
      Name = ticketPriority.Name
    }).ToList();
  }
}
