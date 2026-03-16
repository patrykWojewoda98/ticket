using System;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketPriorityCommands.CreateTicketPriority;

public class CreateTicketPriorityCommandHandler : IRequestHandler<CreateTicketPriorityCommand, TicketPriorityDto>
{
  private readonly ITicketPriorityRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public CreateTicketPriorityCommandHandler(ITicketPriorityRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<TicketPriorityDto> Handle(CreateTicketPriorityCommand request, CancellationToken cancellationToken)
  {
    var ticketPriority = new TicketPriority
    {
      Name = request.Name
    };

    _repository.CreateEntity(ticketPriority);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new TicketPriorityDto
    {
      Id = ticketPriority.Id,
      Name = ticketPriority.Name
    };
  }
}

