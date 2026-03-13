using System;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Queries.BaseQueries.GetAllEntities;

public class GetAllEntitiesQueryHandler<T> : IRequestHandler<GetAllEntitiesQuery<T>, List<T>> where T : Base
{
  private readonly IBaseRepository<T> _repository;

  public GetAllEntitiesQueryHandler(IBaseRepository<T> repository)
  {
    _repository = repository;
  }

  public async Task<List<T>> Handle(GetAllEntitiesQuery<T> request, CancellationToken cancellationToken)
  {
    return await _repository.GetAllAsync();
  }
}
