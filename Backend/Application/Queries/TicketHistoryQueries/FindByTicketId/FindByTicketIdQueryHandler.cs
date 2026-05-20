using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketHistoryQueries.FindByTicketId;

public class FindByTicketIdQueryHandler : IRequestHandler<FindByTicketIdQuery, List<TicketHistoryDto>>
{
  private readonly ITicketHistoryRepository _repository;

  public FindByTicketIdQueryHandler(ITicketHistoryRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<TicketHistoryDto>> Handle(FindByTicketIdQuery request, CancellationToken cancellationToken)
  {
    var ticketHistories = await _repository.FindByTicketIdAsync(request.TicketId);
    return ticketHistories.Select(ticketHistory => new TicketHistoryDto
    {
      Id = ticketHistory.Id,
      TicketId = ticketHistory.TicketId,
      UserId = ticketHistory.UserId,
      Action = ticketHistory.Action,
      OldValue = ticketHistory.OldValue,
      NewValue = ticketHistory.NewValue
    }).ToList();
  }
}
