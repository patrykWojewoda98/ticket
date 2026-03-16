using System;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketStatusCommands.CreateTicketStatus;

public class CreateTicketStatusCommandHandler : IRequestHandler<CreateTicketStatusCommand, TicketStatusDto>
{
  private readonly ITicketStatusRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public CreateTicketStatusCommandHandler(ITicketStatusRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<TicketStatusDto> Handle(CreateTicketStatusCommand request, CancellationToken cancellationToken)
  {
    var ticketStatus = new TicketStatus
    {
      Name = request.Name
    };

    _repository.CreateEntity(ticketStatus);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new TicketStatusDto
    {
      Id = ticketStatus.Id,
      Name = ticketStatus.Name
    };
  }
}

