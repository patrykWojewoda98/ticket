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

  public virtual async void CreateAsync(T entity)
  {
    await _dbSet.AddAsync(entity);
    await _dbContext.SaveChangesAsync();
  }

  public virtual async void UpdateAsync(T entity)
  {
    var existing = await _dbSet.FindAsync(entity.Id);
    if (existing != null)
    {
      _dbContext.Entry(existing).CurrentValues.SetValues(entity);
      await _dbContext.SaveChangesAsync();
    }
  }

  public virtual async void DeleteAsync(int id)
  {
    var existing = await _dbSet.FindAsync(id);
    if (existing != null)
    {
      _dbSet.Remove(existing);
      await _dbContext.SaveChangesAsync();
    }
  }
}
