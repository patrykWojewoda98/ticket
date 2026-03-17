using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketHistoryQueries.GetTicketHistoryById;

public class GetTicketHistoryByIdQueryHandler : IRequestHandler<GetTicketHistoryByIdQuery, TicketHistoryDto>
{
  private readonly ITicketHistoryRepository _repository;

  public GetTicketHistoryByIdQueryHandler(ITicketHistoryRepository repository)
  {
    _repository = repository;
  }

  public async Task<TicketHistoryDto> Handle(GetTicketHistoryByIdQuery request, CancellationToken cancellationToken)
  {
    var ticketHistory = await _repository.GetByIdAsync(request.Id);
    if (ticketHistory == null) return null;

    return new TicketHistoryDto
    {
      Id = ticketHistory.Id,
      TicketId = ticketHistory.TicketId,
      UserId = ticketHistory.UserId,
      Action = ticketHistory.Action,
      OldValue = ticketHistory.OldValue,
      NewValue = ticketHistory.NewValue
    };
  }
}
