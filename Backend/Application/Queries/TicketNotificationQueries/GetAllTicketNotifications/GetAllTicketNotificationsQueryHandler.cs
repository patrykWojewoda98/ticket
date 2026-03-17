using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketNotificationQueries.GetAllTicketNotifications;

public class GetAllTicketNotificationsQueryHandler : IRequestHandler<GetAllTicketNotificationsQuery, List<TicketNotificationDto>>
{
  private readonly ITicketNotificationRepository _repository;

  public GetAllTicketNotificationsQueryHandler(ITicketNotificationRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<TicketNotificationDto>> Handle(GetAllTicketNotificationsQuery request, CancellationToken cancellationToken)
  {
    var ticketNotifications = await _repository.GetAllAsync();
    return ticketNotifications.Select(ticketNotification => new TicketNotificationDto
    {
      Id = ticketNotification.Id,
      TicketId = ticketNotification.TicketId,
      UserId = ticketNotification.UserId,
      Message = ticketNotification.Message,
      Read = ticketNotification.Read
    }).ToList();
  }
}
