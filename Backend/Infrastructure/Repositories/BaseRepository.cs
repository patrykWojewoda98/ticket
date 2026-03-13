using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : Base
{
  protected readonly DatabaseContext _dbContext;
  protected readonly DbSet<T> _dbSet;

  public BaseRepository(DatabaseContext context)
  {
    _dbContext = context;
    _dbSet = context.Set<T>();
  }

  public virtual async Task<List<T>> GetAllAsync()
  {
    return await _dbSet.ToListAsync();
  }

  public virtual async Task<T?> GetByIdAsync(int id)
  {
    return await _dbSet.FindAsync(id);
  }

  public virtual void CreateEntity(T entity)
  {
    _dbSet.Add(entity);
  }

  public virtual void UpdateEntity(T entity)
  {
    _dbSet.Update(entity);
  }

  public virtual void DeleteEntity(T entity)
  {
    _dbSet.Remove(entity);
  }
}
