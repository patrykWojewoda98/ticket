using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.TicketNotificationCommands.UpdateTicketNotification;

public class UpdateTicketNotificationCommandHandler : IRequestHandler<UpdateTicketNotificationCommand, TicketNotificationDto>
{
  private readonly ITicketNotificationRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public UpdateTicketNotificationCommandHandler(ITicketNotificationRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }
  public async Task<TicketNotificationDto> Handle(UpdateTicketNotificationCommand request, CancellationToken cancellationToken)
  {
    var ticketNotification = await _repository.GetByIdAsync(request.TicketNotificationId, cancellationToken);
    if (ticketNotification == null) return null;

    ticketNotification.Message = request.Message;
    ticketNotification.Read = request.Read.Value;
    ticketNotification.TicketId = request.TicketId;
    ticketNotification.UserId = request.UserId;

    _repository.UpdateEntity(ticketNotification);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
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
