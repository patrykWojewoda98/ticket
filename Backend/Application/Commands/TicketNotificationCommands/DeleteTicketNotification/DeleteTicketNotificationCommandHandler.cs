using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.TicketNotificationCommands.DeleteTicketNotification;

public class DeleteTicketNotificationCommandHandler : IRequestHandler<DeleteTicketNotificationCommand, TicketNotificationDto>
{
  private readonly ITicketNotificationRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public DeleteTicketNotificationCommandHandler(ITicketNotificationRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<TicketNotificationDto> Handle(DeleteTicketNotificationCommand request, CancellationToken cancellationToken)
  {
    var ticketNotification = await _repository.GetByIdAsync(request.TicketNotificationId, cancellationToken);
    if (ticketNotification == null) return null;

    var TicketNotificationDto = new TicketNotificationDto
    {
      Id = ticketNotification.Id,
      TicketId = ticketNotification.TicketId,
      UserId = ticketNotification.UserId,
      Message = ticketNotification.Message,
      Read = ticketNotification.Read
    };

    _repository.DeleteEntity(ticketNotification);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return TicketNotificationDto;
  }
}
