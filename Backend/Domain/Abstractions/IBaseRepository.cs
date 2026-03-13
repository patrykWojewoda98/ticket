using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IBaseRepository<T> where T : Base
{
  Task<List<T>> GetAllAsync();
  Task<T?> GetByIdAsync(int id);
  void CreateEntity(T entity);
  void UpdateEntity(T entity);
  void DeleteEntity(T entity);
}
