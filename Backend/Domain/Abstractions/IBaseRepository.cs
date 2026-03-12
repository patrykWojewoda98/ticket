using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IBaseRepository<T> where T : Base
{
  Task<List<T>> GetAllAsync();
  Task<T?> GetByIdAsync(int id);
  void CreateAsync(T entity);
  void UpdateAsync(T entity);
  void DeleteAsync(int id);
}
