using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.TicketPriorityCommands.UpdateTicketPriority;

public class UpdateTicketPriorityCommandHandler : IRequestHandler<UpdateTicketPriorityCommand, TicketPriorityDto>
{
  private readonly ITicketPriorityRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public UpdateTicketPriorityCommandHandler(ITicketPriorityRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }
  public async Task<TicketPriorityDto> Handle(UpdateTicketPriorityCommand request, CancellationToken cancellationToken)
  {
    var ticketPriority = await _repository.GetByIdAsync(request.TicketPriorityId, cancellationToken);
    if (ticketPriority == null) return null;

    ticketPriority.Name = request.Name;

    _repository.UpdateEntity(ticketPriority);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new TicketPriorityDto
    {
      Id = ticketPriority.Id,
      Name = ticketPriority.Name
    };
  }
}
