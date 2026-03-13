using System;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Queries.BaseQueries.GetEntityById;

public class GetEntityByIdQueryHandler<T> : IRequestHandler<GetEntityByIdQuery<T>, T?> where T : Base
{
  private readonly IBaseRepository<T> _repository;

  public GetEntityByIdQueryHandler(IBaseRepository<T> repository)
  {
    _repository = repository;
  }

  public async Task<T?> Handle(GetEntityByIdQuery<T> request, CancellationToken cancellationToken)
  {
    return await _repository.GetByIdAsync(request.Id);
  }
}
