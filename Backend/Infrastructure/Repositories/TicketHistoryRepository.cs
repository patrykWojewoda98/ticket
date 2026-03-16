using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketHistoryRepository : BaseRepository<TicketHistory>, ITicketHistoryRepository
{
  public TicketHistoryRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<TicketHistory>> FindByTicketIdAsync(int ticketId, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<TicketHistory>()
                 .Where(ticketHistory => ticketHistory.TicketId == ticketId)
                 .ToListAsync(cancellationToken);
  }
}
