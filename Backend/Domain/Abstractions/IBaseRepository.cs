using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IBaseRepository<T> where T : Base
{
  Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
  Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
  void CreateEntity(T entity);
  void UpdateEntity(T entity);
  void DeleteEntity(T entity);
}
