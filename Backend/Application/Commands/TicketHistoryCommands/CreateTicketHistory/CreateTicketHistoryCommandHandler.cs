using System;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketHistoryCommands.CreateTicketHistory;

public class CreateTicketHistoryCommandHandler : IRequestHandler<CreateTicketHistoryCommand, TicketHistoryDto>
{
  private readonly ITicketHistoryRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public CreateTicketHistoryCommandHandler(ITicketHistoryRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<TicketHistoryDto> Handle(CreateTicketHistoryCommand request, CancellationToken cancellationToken)
  {
    var ticketHistory = new TicketHistory
    {
      TicketId = request.TicketId,
      UserId = request.UserId,
      Action = request.Action,
      OldValue = request.OldValue,
      NewValue = request.NewValue
    };

    _repository.CreateEntity(ticketHistory);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
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

