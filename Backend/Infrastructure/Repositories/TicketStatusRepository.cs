using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketStatusRepository : BaseRepository<TicketStatus>, ITicketStatusRepository
{
  public TicketStatusRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<TicketStatus?> FindByNameAsync(string name)
  {
    return await _dbSet
                 .FirstOrDefaultAsync(ticketStatus => ticketStatus.Name == name);
  }
}
