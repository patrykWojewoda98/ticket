using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketHistoryRepository : BaseRepository<TicketHistory>, ITicketHistoryRepository
{
  public TicketHistoryRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<TicketHistory>> FindByTicketIdAsync(int ticketId)
  {
    return await _dbSet
                 .Where(ticketHistory => ticketHistory.TicketId == ticketId)
                 .ToListAsync();
  }
}
