using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.TicketHistoryCommands.DeleteTicketHistory;

public class DeleteTicketHistoryCommandHandler : IRequestHandler<DeleteTicketHistoryCommand, TicketHistoryDto>
{
  private readonly ITicketHistoryRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public DeleteTicketHistoryCommandHandler(ITicketHistoryRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<TicketHistoryDto> Handle(DeleteTicketHistoryCommand request, CancellationToken cancellationToken)
  {
    var ticketHistory = await _repository.GetByIdAsync(request.TicketHistoryId, cancellationToken);
    if (ticketHistory == null) return null;

    var TicketHistoryDto = new TicketHistoryDto
    {
      Id = ticketHistory.Id,
      TicketId = ticketHistory.TicketId,
      UserId = ticketHistory.UserId,
      Action = ticketHistory.Action,
      OldValue = ticketHistory.OldValue,
      NewValue = ticketHistory.NewValue
    };

    _repository.DeleteEntity(ticketHistory);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return TicketHistoryDto;
  }
}
