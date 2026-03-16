using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.TicketStatusCommands.DeleteTicketStatus;

public class DeleteTicketStatusCommandHandler : IRequestHandler<DeleteTicketStatusCommand, TicketStatusDto>
{
  private readonly ITicketStatusRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public DeleteTicketStatusCommandHandler(ITicketStatusRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<TicketStatusDto> Handle(DeleteTicketStatusCommand request, CancellationToken cancellationToken)
  {
    var ticketStatus = await _repository.GetByIdAsync(request.TicketStatusId, cancellationToken);
    if (ticketStatus == null) return null;

    var TicketStatusDto = new TicketStatusDto
    {
      Id = ticketStatus.Id,
      Name = ticketStatus.Name
    };

    _repository.DeleteEntity(ticketStatus);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return TicketStatusDto;
  }
}
