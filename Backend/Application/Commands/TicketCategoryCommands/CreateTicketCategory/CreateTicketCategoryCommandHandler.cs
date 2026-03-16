using System;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketCategoryCommands.CreateTicketCategory;

public class CreateTicketCategoryCommandHandler : IRequestHandler<CreateTicketCategoryCommand, TicketCategoryDto>
{
  private readonly ITicketCategoryRepository _repository;
  private readonly IUnitOfWorkService _unitOfWork;

  public CreateTicketCategoryCommandHandler(ITicketCategoryRepository repository, IUnitOfWorkService unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<TicketCategoryDto> Handle(CreateTicketCategoryCommand request, CancellationToken cancellationToken)
  {
    var ticketCategory = new TicketCategory
    {
      Name = request.Name
    };

    _repository.CreateEntity(ticketCategory);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return new TicketCategoryDto
    {
      Id = ticketCategory.Id,
      Name = ticketCategory.Name
    };
  }
}

