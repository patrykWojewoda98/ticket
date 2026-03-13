using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketNotificationQueries._FindByTicketId;

public class FindByTicketIdQueryHandler : IRequestHandler<FindByTicketIdQuery, List<TicketNotificationDto>>
{
  private readonly ITicketNotificationRepository _repository;

  public FindByTicketIdQueryHandler(ITicketNotificationRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<TicketNotificationDto>> Handle(FindByTicketIdQuery request, CancellationToken cancellationToken)
  {
    var ticketHistories = await _repository.FindByTicketIdAsync(request.TicketId);
    return ticketHistories.Select(ticketNotification => new TicketNotificationDto
    {
      Id = ticketNotification.Id,
      TicketId = ticketNotification.TicketId,
      UserId = ticketNotification.UserId,
      Message = ticketNotification.Message,
      Read = ticketNotification.Read
    }).ToList();
  }
}
