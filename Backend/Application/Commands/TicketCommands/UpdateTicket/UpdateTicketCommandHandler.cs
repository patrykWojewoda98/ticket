using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.TicketCommands.UpdateTicket;

public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, TicketDto>
{
  private readonly ITicketRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public UpdateTicketCommandHandler(ITicketRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }
  public async Task<TicketDto> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
  {
    var ticket = await _repository.GetByIdAsync(request.TicketId, cancellationToken);
    if (ticket == null) return null;

    ticket.Title = request.Title;
    ticket.Description = request.Description;
    ticket.StatusId = request.StatusId.Value;
    ticket.PriorityId = request.PriorityId.Value;
    ticket.CategoryId = request.CategoryId;
    ticket.AssigneeId = request.AssigneeId;

    _repository.UpdateEntity(ticket);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new TicketDto
    {
      Id = ticket.Id,
      UserId = ticket.UserId,
      AssigneeId = ticket.AssigneeId,
      CategoryId = ticket.CategoryId,
      StatusId = ticket.StatusId,
      PriorityId = ticket.PriorityId,
      Title = ticket.Title,
      Description = ticket.Description
    };
  }
}
