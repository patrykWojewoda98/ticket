using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : Base
{
  protected readonly DatabaseContext _dbContext;

  public BaseRepository(DatabaseContext context)
  {
    _dbContext = context;
  }

  public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<T>().ToListAsync(cancellationToken);
  }

  public virtual async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<T>()
        .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
  }

  public virtual void CreateEntity(T entity)
  {
    _dbContext.Set<T>().Add(entity);
  }

  public virtual void UpdateEntity(T entity)
  {
    _dbContext.Set<T>().Update(entity);
  }

  public virtual void DeleteEntity(T entity)
  {
    _dbContext.Set<T>().Remove(entity);
  }
}
