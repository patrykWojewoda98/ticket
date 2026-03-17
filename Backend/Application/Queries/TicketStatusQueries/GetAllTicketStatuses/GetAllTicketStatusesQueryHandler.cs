using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketStatusQueries.GetAllTicketStatuses;

public class GetAllTicketStatusesQueryHandler : IRequestHandler<GetAllTicketStatusesQuery, List<TicketStatusDto>>
{
  private readonly ITicketStatusRepository _repository;

  public GetAllTicketStatusesQueryHandler(ITicketStatusRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<TicketStatusDto>> Handle(GetAllTicketStatusesQuery request, CancellationToken cancellationToken)
  {
    var ticketStatuses = await _repository.GetAllAsync();
    return ticketStatuses.Select(ticketStatus => new TicketStatusDto
    {
      Id = ticketStatus.Id,
      Name = ticketStatus.Name
    }).ToList();
  }
}
