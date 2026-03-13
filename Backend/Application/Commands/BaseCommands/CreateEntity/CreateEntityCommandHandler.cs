using System;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.BaseCommands.CreateEntity;

public class CreateEntityCommandHandler<T> : IRequestHandler<CreateEntityCommand<T>, Unit> where T : Base
{
  private readonly IBaseRepository<T> _repository;
  private readonly IUnitOfWorkRepository _unitOfWork;

  public CreateEntityCommandHandler(IBaseRepository<T> repository, IUnitOfWorkRepository unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Unit> Handle(CreateEntityCommand<T> request, CancellationToken cancellationToken)
  {
    _repository.CreateEntity(request.Entity);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return Unit.Value;
  }
}
