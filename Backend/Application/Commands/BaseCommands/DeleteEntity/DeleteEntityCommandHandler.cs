using System;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.BaseCommands.DeleteEntity;

public class DeleteEntityCommandHandler<T> : IRequestHandler<DeleteEntityCommand<T>, Unit> where T : Base
{
  private readonly IBaseRepository<T> _repository;
  private readonly IUnitOfWorkRepository _unitOfWork;

  public DeleteEntityCommandHandler(IBaseRepository<T> repository, IUnitOfWorkRepository unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Unit> Handle(DeleteEntityCommand<T> request, CancellationToken cancellationToken)
  {
    _repository.DeleteEntity(request.Entity);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return Unit.Value;
  }
}
