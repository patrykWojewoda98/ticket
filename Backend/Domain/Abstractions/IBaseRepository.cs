using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IBaseRepository<T> where T : Base
{
  Task<List<T>> GetAllAsync();
  Task<T?> GetByIdAsync(int id);
  void Create(T entity);
  void Update(T entity);
  void Delete(T entity);
}
