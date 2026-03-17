using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketHistoryQueries.GetAllTicketHistories;

public class GetAllTicketHistoriesQueryHandler : IRequestHandler<GetAllTicketHistoriesQuery, List<TicketHistoryDto>>
{
  private readonly ITicketHistoryRepository _repository;

  public GetAllTicketHistoriesQueryHandler(ITicketHistoryRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<TicketHistoryDto>> Handle(GetAllTicketHistoriesQuery request, CancellationToken cancellationToken)
  {
    var ticketHistories = await _repository.GetAllAsync();
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
