using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.TicketHistoryCommands.UpdateTicketHistory;

public class UpdateTicketHistoryCommandHandler : IRequestHandler<UpdateTicketHistoryCommand, TicketHistoryDto>
{
  private readonly ITicketHistoryRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public UpdateTicketHistoryCommandHandler(ITicketHistoryRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }
  public async Task<TicketHistoryDto> Handle(UpdateTicketHistoryCommand request, CancellationToken cancellationToken)
  {
    var ticketHistory = await _repository.GetByIdAsync(request.TicketId, cancellationToken);
    if (ticketHistory == null) return null;

    ticketHistory.TicketId = request.TicketId;
    ticketHistory.UserId = request.UserId;
    ticketHistory.Action = request.Action;
    ticketHistory.OldValue = request.OldValue;
    ticketHistory.NewValue = request.NewValue;

    _repository.UpdateEntity(ticketHistory);
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
