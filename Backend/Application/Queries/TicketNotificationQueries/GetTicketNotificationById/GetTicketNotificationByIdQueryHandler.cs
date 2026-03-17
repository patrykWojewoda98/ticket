using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketNotificationQueries.GetTicketNotificationById;

public class GetTicketNotificationByIdQueryHandler : IRequestHandler<GetTicketNotificationByIdQuery, TicketNotificationDto>
{
  private readonly ITicketNotificationRepository _repository;

  public GetTicketNotificationByIdQueryHandler(ITicketNotificationRepository repository)
  {
    _repository = repository;
  }

  public async Task<TicketNotificationDto> Handle(GetTicketNotificationByIdQuery request, CancellationToken cancellationToken)
  {
    var ticketNotification = await _repository.GetByIdAsync(request.Id);
    if (ticketNotification == null) return null;

    return new TicketNotificationDto
    {
      Id = ticketNotification.Id,
      TicketId = ticketNotification.TicketId,
      UserId = ticketNotification.UserId,
      Message = ticketNotification.Message,
      Read = ticketNotification.Read
    };
  }
}
