using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketCategoryRepository : BaseRepository<TicketCategory>, ITicketCategoryRepository
{
  public TicketCategoryRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<TicketCategory?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<TicketCategory>()
                 .FirstOrDefaultAsync(ticketCategory => ticketCategory.Name == name, cancellationToken);
  }
}
