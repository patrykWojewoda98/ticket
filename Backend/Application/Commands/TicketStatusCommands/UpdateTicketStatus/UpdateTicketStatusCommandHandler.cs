using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.TicketStatusCommands.UpdateTicketStatus;

public class UpdateTicketStatusCommandHandler : IRequestHandler<UpdateTicketStatusCommand, TicketStatusDto>
{
  private readonly ITicketStatusRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public UpdateTicketStatusCommandHandler(ITicketStatusRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }
  public async Task<TicketStatusDto> Handle(UpdateTicketStatusCommand request, CancellationToken cancellationToken)
  {
    var ticketStatus = await _repository.GetByIdAsync(request.TicketStatusId, cancellationToken);
    if (ticketStatus == null) return null;

    ticketStatus.Name = request.Name;

    _repository.UpdateEntity(ticketStatus);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new TicketStatusDto
    {
      Id = ticketStatus.Id,
      Name = ticketStatus.Name
    };
  }
}
