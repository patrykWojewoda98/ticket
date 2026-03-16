using System;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketNotificationCommands.CreateTicketNotification;

public class CreateTicketNotificationCommandHandler : IRequestHandler<CreateTicketNotificationCommand, TicketNotificationDto>
{
  private readonly ITicketNotificationRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public CreateTicketNotificationCommandHandler(ITicketNotificationRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<TicketNotificationDto> Handle(CreateTicketNotificationCommand request, CancellationToken cancellationToken)
  {
    var ticketNotification = new TicketNotification
    {
      TicketId = request.TicketId,
      UserId = request.UserId,
      Message = request.Message,
      Read = false
    };

    _repository.CreateEntity(ticketNotification);
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

