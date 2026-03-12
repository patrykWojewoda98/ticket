using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketCategoryRepository : BaseRepository<TicketCategory>, ITicketCategoryRepository
{
  public TicketCategoryRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<TicketCategory?> FindByNameAsync(string name)
  {
    return await _dbSet
                 .FirstOrDefaultAsync(ticketCategory => ticketCategory.Name == name);
  }
}
