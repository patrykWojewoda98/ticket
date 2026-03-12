using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IBaseRepository<T> where T : Base
{
  Task<List<T>> GetAllAsync();
  Task<T?> GetByIdAsync(string id);
  Task<T> CreateAsync(T entity);
  Task<T> UpdateAsync(T entity);
  Task DeleteAsync(string id);
}
