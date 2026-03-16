using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.TicketPriorityCommands.DeleteTicketPriority;

public class DeleteTicketPriorityCommandHandler : IRequestHandler<DeleteTicketPriorityCommand, TicketPriorityDto>
{
  private readonly ITicketPriorityRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public DeleteTicketPriorityCommandHandler(ITicketPriorityRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<TicketPriorityDto> Handle(DeleteTicketPriorityCommand request, CancellationToken cancellationToken)
  {
    var ticketPriority = await _repository.GetByIdAsync(request.TicketPriorityId, cancellationToken);
    if (ticketPriority == null) return null;

    var TicketPriorityDto = new TicketPriorityDto
    {
      Id = ticketPriority.Id,
      Name = ticketPriority.Name
    };

    _repository.DeleteEntity(ticketPriority);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return TicketPriorityDto;
  }
}
