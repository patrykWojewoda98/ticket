using System;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Commands.BaseCommands.UpdateEntity;

public class UpdateEntityCommandHandler<T> : IRequestHandler<UpdateEntityCommand<T>, Unit> where T : Base
{
  private readonly IBaseRepository<T> _repository;
  private readonly IUnitOfWorkRepository _unitOfWork;

  public UpdateEntityCommandHandler(IBaseRepository<T> repository, IUnitOfWorkRepository unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Unit> Handle(UpdateEntityCommand<T> request, CancellationToken cancellationToken)
  {
    _repository.UpdateEntity(request.Entity);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return Unit.Value;
  }
}
