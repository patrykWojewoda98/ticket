using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UnitOfWorkRepository : IUnitOfWorkRepository
{
  protected readonly DatabaseContext _dbContext;

  public UnitOfWorkRepository(DatabaseContext context)
  {
    _dbContext = context;
  }

  public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    var entries = _dbContext.ChangeTracker
        .Entries<Base>()
        .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified);

    foreach (var entry in entries)
    {
      if (entry.State == EntityState.Added)
      {
        entry.Entity.CreatedAt = DateTime.UtcNow;
      }
      else if (entry.State == EntityState.Modified)
      {
        entry.Entity.UpdatedAt = DateTime.UtcNow;
      }
    }

    await _dbContext.SaveChangesAsync(cancellationToken);
  }
}
