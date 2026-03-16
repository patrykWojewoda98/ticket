using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.TicketCategoryCommands.DeleteTicketCategory;

public class DeleteTicketCategoryCommandHandler : IRequestHandler<DeleteTicketCategoryCommand, TicketCategoryDto>
{
  private readonly ITicketCategoryRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public DeleteTicketCategoryCommandHandler(ITicketCategoryRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<TicketCategoryDto> Handle(DeleteTicketCategoryCommand request, CancellationToken cancellationToken)
  {
    var ticketCategory = await _repository.GetByIdAsync(request.TicketCategoryId, cancellationToken);
    if (ticketCategory == null) return null;

    var TicketCategoryDto = new TicketCategoryDto
    {
      Id = ticketCategory.Id,
      Name = ticketCategory.Name
    };

    _repository.DeleteEntity(ticketCategory);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return TicketCategoryDto;
  }
}
