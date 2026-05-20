using System;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketCommands.CreateTicket;

public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, TicketDto>
{
  private readonly ITicketRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public CreateTicketCommandHandler(ITicketRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<TicketDto> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
  {
    var ticket = new Ticket
    {
      UserId = request.UserId,
      Title = request.Title,
      Description = request.Description,
      StatusId = request.StatusId,
      PriorityId = request.PriorityId,
      CategoryId = request.CategoryId,
      AssigneeId = request.AssigneeId
    };

    _repository.CreateEntity(ticket);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new TicketDto
    {
      Id = ticket.Id,
      UserId = ticket.UserId,
      Title = ticket.Title,
      Description = ticket.Description,
      StatusId = ticket.StatusId,
      PriorityId = ticket.PriorityId,
      CategoryId = ticket.CategoryId,
      AssigneeId = ticket.AssigneeId
    };
  }
}

