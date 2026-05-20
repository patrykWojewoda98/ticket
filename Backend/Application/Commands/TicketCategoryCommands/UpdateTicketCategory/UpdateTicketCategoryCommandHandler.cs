using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Commands.TicketCategoryCommands.UpdateTicketCategory;

public class UpdateTicketCategoryCommandHandler : IRequestHandler<UpdateTicketCategoryCommand, TicketCategoryDto>
{
  private readonly ITicketCategoryRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public UpdateTicketCategoryCommandHandler(ITicketCategoryRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }
  public async Task<TicketCategoryDto> Handle(UpdateTicketCategoryCommand request, CancellationToken cancellationToken)
  {
    var ticketCategory = await _repository.GetByIdAsync(request.TicketCategoryId, cancellationToken);
    if (ticketCategory == null) return null;

    ticketCategory.Name = request.Name;

    _repository.UpdateEntity(ticketCategory);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new TicketCategoryDto
    {
      Id = ticketCategory.Id,
      Name = ticketCategory.Name
    };
  }
}
