using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.TicketCommands.DeleteTicket;

public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, TicketDto>
{
  private readonly ITicketRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public DeleteTicketCommandHandler(ITicketRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<TicketDto> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
  {
    var ticket = await _repository.GetByIdAsync(request.TicketId, cancellationToken);
    if (ticket == null) return null;

    var TicketDto = new TicketDto
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

    _repository.DeleteEntity(ticket);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return TicketDto;
  }
}
